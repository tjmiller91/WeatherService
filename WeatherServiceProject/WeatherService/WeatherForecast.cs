using System.Text.Json.Serialization;

namespace WeatherService
{
    public class WeatherForecast
    {
        public string? date { get; set; }
        public Temperature temperature { get; set; }
        public string? summary { get; set; }
        [JsonIgnore]
        public string? description { get; set; }

        public class Temperature
        {
            public double celsius { get; set; }
            public double fahrenheit { get; set; }
        }

        public WeatherForecast()
        {
            temperature = new Temperature();
        }

    }

    public class WeatherForecastDTO
    {
        public List<Data> data { get; set; }
        public class Data
        {
            public string? datetime { get; set; }
            public double temp { get; set; }
            public Weather? weather { get; set; }

            public class Weather
            {
                public string? description { get; set; }
                public string? code { get; set; }
                public string? icon { get; set; }

            }
        }

        public WeatherForecastDTO()
        {
            data = new List<Data>();
        }
    }
}