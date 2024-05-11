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
            return true;
        }

        public void Execute(object parameter)
        {
            //  current view model to HomeVM

        }
    }
}
