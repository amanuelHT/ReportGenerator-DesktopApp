using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class AddReportVM : ViewModelBase
    {
        public ReportFormVM ReportFormVM { get; }
        public AddReportVM(ReportStore reportStore, ModalNavigation navigationStore)
        {
            ICommand submitCommand = new AddReportCommand(this, reportStore, navigationStore);

            ICommand cancelCommand = new CloseModalCommand(navigationStore);

            ReportFormVM = new ReportFormVM(submitCommand, cancelCommand);


        }
    }
}
