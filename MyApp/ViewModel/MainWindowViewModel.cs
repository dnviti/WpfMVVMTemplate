using Microsoft.Extensions.Logging;
using MyApp.Helpers;
using MyApp.Interfaces;
using MyApp.Services;
using System.Windows.Input;

namespace MyApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ILogger<MainWindowViewModel> logger;
        private readonly IMyService myService;

        private string retString = string.Empty;

        public string RetString
        {
            get { return retString; }
            set { retString = value; OnPropertyChanged(); }
        }

        public ICommand GetStringCommand { get; private set; } = null!;

        public MainWindowViewModel()
        {

        }
        public MainWindowViewModel(ILogger<MainWindowViewModel> logger, IMyService myService)
        {
            this.logger = logger;
            this.myService = myService;
            GetStringCommand = new Command(() =>
            {
                RetString = this.myService.ReturnStringMethod("PROVA");
            });
        }

    }
}
