using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    class EditReportVM : ViewModelBase
    {
        public ReportFormVM ReportFormVM { get; }
        public EditReportVM(NavigationStore navigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(navigationStore);
            ReportFormVM = new ReportFormVM(null, cancelCommand);

        }
    }
}
