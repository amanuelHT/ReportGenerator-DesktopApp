using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class AddReportVM : ViewModelBase
    {
        public ReportFormVM ReportFormVM { get; }
        public AddReportVM(NavigationStore navigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(navigationStore);
            ReportFormVM = new ReportFormVM(null, cancelCommand);


        }
    }
}
