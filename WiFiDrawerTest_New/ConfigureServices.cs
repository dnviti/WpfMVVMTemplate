using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyApp.Pages;
using MyApp.Services;
using MyApp.ViewModel;
using Serilog;
using System.Diagnostics;
using System.IO;

namespace MyApp
{
    public static class ConfigureServices
    {
        public static ServiceProvider GetServiceProvider()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

            if (Debugger.IsAttached)
                builder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
            else
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            var serviceCollection = new ServiceCollection();
            Configure(config, serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        public static void Configure(IConfigurationRoot config, IServiceCollection services)
        {
            #region Configure Settings
            services.Configure<AppSettings>(config);

            // Get settings to use in this configuration if needed
            var settings = config.Get<AppSettings>();
            if (settings is null) throw new FileNotFoundException("Unable to load configuration");
            #endregion Configure Settings

            #region Configure Logging
            var seriLog = new LoggerConfiguration()
                   .ReadFrom.Configuration(config)
                   .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog(seriLog);
            // Create global logger
            var logger = loggerFactory.CreateLogger("Logger");
            // Register logger factory
            services.AddSingleton(loggerFactory);
            // Register global logger
            services.AddSingleton(logger);
            #endregion Configure Logging

            #region Configure Services
            // Nuget Services
            services.AddHttpClient("MyHttpClient", o =>
            {
                o.BaseAddress = new Uri(settings.ApiBaseAddress);
                o.DefaultRequestHeaders.Add("header-name", "value");
            });

            // My Services
            services.AddSingleton<AlertService>();
            services.AddSingleton<MyService>();
            #endregion Configure Services

            #region Configure ViewModels
            services.AddSingleton<MainWindowViewModel>();
            #endregion Configure ViewModels

            #region Configure Pages
            // Pages
            services.AddSingleton<MainWindow>();
            #endregion Configure Pages
        }
    }
}
