using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class EditReportVM : ViewModelBase
    {
        private readonly ReportStore _reportStore;

        public Guid ReportId { get; set; }
        public ReportFormVM ReportFormVM { get; }

        public EditReportVM(ReportModel reportModel, ReportStore reportStore, ModalNavigation navigationStore)
        {

            _reportStore = reportStore;
            if (reportModel == null)
            {
                throw new ArgumentNullException(nameof(reportModel));
            }

            ReportId = reportModel.Id;

            ICommand submitCommand = new EditReportCommand(this, reportStore, navigationStore);
            ICommand cancelCommand = new CloseModalCommand(navigationStore);

            ReportFormVM = new ReportFormVM(submitCommand, cancelCommand, reportStore)
            {
                Tittle = reportModel.Tittle,
                Status = reportModel.Status,
                Kunde = reportModel.Kunde,
                Images = reportModel.Images,
            };



        }




    }
}
