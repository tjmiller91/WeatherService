using Microsoft.AspNetCore.Mvc;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private static readonly Dictionary<int, string> Summaries = new Dictionary<int, string>(){{0,
            "Freezing" }, {4,"Bracing" }, {10,"Chilly" } , {16,"Cool" }, {21,"Mild" }, {27,"Warm" }, {32,"Hot" }, {38,"Sweltering" }, {43,"Scorching" }
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{postal_code}",Name = "GetForecast")]
        public WeatherForecast Get(string postal_code = "67212")
        {
            WeatherForecast forecast = WeatherService.Implementations.WeatherForecastImplementation.GetWeatherForecast(postal_code);

            //todo: set Summary value on forecast response using Summaries data dictionary
            //I'm not sure why we're using a dictionary when the summary is returned as description.
            //This attempts to look up the code returned and if it's not found, use the given description
            if (Summaries.TryGetValue(Convert.ToInt32(forecast.summary), out string? summaryRet))
                forecast.summary = summaryRet;
            else
                forecast.summary = forecast.description;

            return forecast;
        }
    }
}