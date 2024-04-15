using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class AddReportVM : ObservableObject

    {
        public ReportFormVM ReportFormVM { get; }
        public AddReportVM(HomeVM homeVM, ReportStore reportStore, NavigationStore navigationStore)
        {
            ICommand submitCommand = new AddReportCommand(this, reportStore, navigationStore);

            ICommand cancelCommand = new CloseModalCommand(homeVM, navigationStore);

            ReportFormVM = new ReportFormVM(submitCommand, cancelCommand, reportStore, Guid.Empty);


        }
    }
}
