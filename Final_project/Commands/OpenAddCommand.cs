using Final_project.Components;
using Final_project.Stores;
using Final_project.ViewModels;
using Final_project.Views;
using System.Windows.Input;

namespace Final_project.Commands
{
    public class OpenAddCommand : ICommand
    {
        private readonly HomeVM _homeVM;
        private readonly ReportStore _reportStore;
        private readonly NavigationStore _navigationStore;

        public OpenAddCommand(HomeVM homeVM, ReportStore reportStore, NavigationStore navigationStore)
        {
            _homeVM = homeVM;
            _reportStore = reportStore;
            _navigationStore = navigationStore;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true; // You can add conditions here if needed
        }

        public void Execute(object parameter)
        {
            try
            {
                // Create an instance of the AddReportVM
                AddReportVM addReportVM = new AddReportVM(_homeVM, _reportStore, _navigationStore);

                // Show the window
                ReportWindowHelper.ShowReportWindow(new AddReportView { DataContext = addReportVM }, "Add Report");
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
