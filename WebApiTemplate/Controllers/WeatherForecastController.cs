using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Services;

namespace WebApiTemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationSettings _settings;
        private readonly WebApiService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<ApplicationSettings> options, WebApiService service)
        {
            _logger = logger;
            _settings = options.Value;
            _service = service;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogDebug($"Проверяем внедрение зависимостей, значение параметра из настроек: {_settings.SomeParameter}");
            var response = _service.SomeAction("Проверяем работу сервиса уровня ядра");
            _logger.LogInformation($"Action status: {response}");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}