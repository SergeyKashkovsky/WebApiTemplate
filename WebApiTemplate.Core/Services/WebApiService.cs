using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace WebApiTemplate.Core.Services
{
    /// <summary>
    /// Бизнес-логика приложения
    /// </summary>
    public class WebApiService
    {
        private readonly ApplicationSettings _settings;
        private readonly ILogger<WebApiService> _logger;
        public WebApiService(IOptions<ApplicationSettings> options, ILogger<WebApiService> logger)
        {
            _logger = logger;
            _settings = options.Value ?? throw new ArgumentNullException("ApplicationSettings");
        }
        /// <summary>
        /// Тестируем внедрение зависимостей и журналирование
        /// </summary>
        /// <param name="logText"></param>
        /// <returns></returns>
        public string SomeAction(string logText)
        {
            _logger.LogDebug($"{logText}, значение параметра из настроек: {_settings.SomeParameter}");
            return "Ok";
        }
    }
}
