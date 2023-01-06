namespace WebApiTemplate.Core
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Параметр для демонстрации передачи значения из файла настроек
        /// </summary>
        public string SomeParameter { get; set; }
        /// <summary>
        /// Разрешенные домены - источники запросов для политики CORS
        /// </summary>
        public string[] AllowedOrigins { get; set; }
    }

}
