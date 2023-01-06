using Microsoft.Extensions.Hosting.WindowsServices;
using NLog;
using NLog.Web;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var options = new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = WindowsServiceHelpers.IsWindowsService()
            ? AppContext.BaseDirectory : default
    };
    var builder = WebApplication.CreateBuilder(options);

    // Add services to the container.
    // Установка службы уровня ядра для внедрения зависимости
    builder.Services.AddTransient<WebApiService>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Подключаем настройки
    builder.Services.Configure<ApplicationSettings>(builder.Configuration);
    var settings = builder.Configuration.Get<ApplicationSettings>() ?? throw new ArgumentNullException("ApplicationSettings", "Конфигурация не получена");

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //CORS
    builder.Services.AddCors(options => options.AddPolicy("AllowAny", builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod())
    );

    builder.Services.AddCors(options => options.AddPolicy("AllowSome", builder => builder
        .WithOrigins(settings.AllowedOrigins.ToArray())
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials())
    );

    var app = builder.Build();

#if DEBUG
    app.UseCors("AllowAny");
#else
    app.UseCors("AllowSome");
#endif

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
