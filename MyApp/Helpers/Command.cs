using System.Windows.Input;

namespace MyApp.Helpers
{

    public class Command : ICommand
    {
        private readonly Action execute;

        public Command(Action execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

#pragma warning disable CS0067
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            execute();
        }
    }
}
