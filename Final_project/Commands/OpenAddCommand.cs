using Final_project.Stores;
using Final_project.ViewModels;
using Final_project.Views;
using System.Windows.Input;

namespace Final_project.Commands
{
    public class OpenAddCommand : ICommand
    {
        private readonly ModalWindow _modalWindow;
        private readonly ModalNavigation _modalNavigation;
        private readonly ReportStore _reportStore;
        private readonly NavigationStore _navigationStore;

        public OpenAddCommand(ModalWindow modalWindow,
            ModalNavigation modalNavigation,
            ReportStore reportStore,
            NavigationStore navigationStore)
        {
            _modalWindow = modalWindow;
            _modalNavigation = modalNavigation;
            _reportStore = reportStore;
            _navigationStore = navigationStore;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                // Create an instance of the AddReportVM
                AddReportVM addReportVM = new AddReportVM(_modalWindow, _modalNavigation, _reportStore, _navigationStore);

                // Show the window
                _modalNavigation.ShowReportWindow(new AddReportView { DataContext = addReportVM }, "Add Report");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
