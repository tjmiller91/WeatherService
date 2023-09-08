using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WeatherService.Implementations
{
    public class WeatherForecastImplementation
    {
        internal static WeatherForecast GetWeatherForecast(string postal_code)
        {
            //todo: retreive current temperature from:
            //https://www.weatherbit.io/api/swaggerui/weather-api-v2#!/Current32Weather32Data/get_current_postal_code_postal_code
            //using API Key: 1824631bbfa74729aac7d2d2f1dfdd76

            var client = new HttpClient();
            string URL = "https://api.weatherbit.io/v2.0/current?postal_code=";
            string APIKey = "1824631bbfa74729aac7d2d2f1dfdd76";
            string req = URL + postal_code + "&key=" + APIKey;


            HttpRequestMessage reqMsg = new HttpRequestMessage(HttpMethod.Get, req);
            HttpResponseMessage response = response = client.Send(reqMsg);

            WeatherForecast forecast = new WeatherForecast();

            if (response.IsSuccessStatusCode)
            {
                //Deserialize into a dto
                WeatherForecastDTO forecastDto = JsonConvert.DeserializeObject<WeatherForecastDTO>(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine(forecastDto);

                if (forecastDto != null)
                {
                    forecast.date = forecastDto.data[0].datetime;
                    forecast.temperature.celsius = Math.Round(forecastDto.data[0].temp, 2);
                    forecast.temperature.fahrenheit = Math.Round(forecastDto.data[0].temp * 9 / 5 + 32, 2);
                    forecast.summary = forecastDto.data[0].weather?.code;
                    forecast.description = forecastDto.data[0].weather?.description;
                }
            }

            return forecast;
        }
    }
}
