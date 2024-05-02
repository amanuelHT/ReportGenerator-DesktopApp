using Domain.Models;
using Final_project.Stores;

namespace Final_project.ViewModels
{
    public class ReportListingItemVM : ViewModelBase
    {
        public ReportModel ReportModel { get; private set; }

        public string Tittle => ReportModel.Tittle;
        public bool Status => ReportModel.Status;


        public ReportStore ReportStore { get; }

        public ModalNavigation ModalNavigation { get; }



        public ReportListingItemVM(
            ReportModel reportModel,
            ReportStore reportStore, NavigationStore navigationStore,
            ModalNavigation modalNavigation)
        {

            ReportModel = reportModel;

        }

        internal void UpdateReport(ReportModel reportModel)
        {
            ReportModel = reportModel;
            OnPropertyChanged(nameof(Tittle));
            OnPropertyChanged(nameof(Status));
        }
    }
}