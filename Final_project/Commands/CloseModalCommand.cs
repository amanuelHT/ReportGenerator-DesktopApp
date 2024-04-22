using System.Windows.Input;

namespace Final_project.Commands
{
    public class CloseModalCommand : ICommand
    {

        private readonly NavigationStore _navigationStore;

        public CloseModalCommand(NavigationStore navigationStore)
        {

            _navigationStore = navigationStore;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true; // You can add conditions here if needed
        }

        public void Execute(object parameter)
        {
            // Set the current view model to HomeVM

        }
    }
}
