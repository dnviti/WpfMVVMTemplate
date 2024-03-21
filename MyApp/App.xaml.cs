using Microsoft.Extensions.DependencyInjection;
using MyApp.Pages;
using System.Windows;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider sp;

        public App()
        {
            sp = ConfigureServices.GetServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = sp.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
