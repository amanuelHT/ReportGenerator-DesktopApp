using Final_project.Commands;
using Final_project.Models;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class EditReportVM : ViewModelBase
    {
        public ReportFormVM ReportFormVM { get; }
        public EditReportVM(ReportModel reportModel, NavigationStore navigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(navigationStore);
            ReportFormVM = new ReportFormVM(null, cancelCommand)

            {
                Tittle = reportModel.Tittle,
                Status = reportModel.Status,
                Kunde = reportModel.Kunde,

            };
        }
    }
}
