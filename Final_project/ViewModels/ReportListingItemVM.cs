using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ReportListingItemVM : ViewModelBase
    {
        public ReportModel ReportModel { get; private set; }

        public string Tittle => ReportModel.Tittle;
        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; }
        public ReportStore ReportStore { get; }
        public ModalNavigation ModalNavigation { get; }



        public ReportListingItemVM(ReportModel reportModel, ReportStore reportStore, ModalNavigation navigationStore)
        {
            ReportModel = reportModel;

            EditCommand = new OpenEditCommand(this, reportStore, navigationStore);
        }

        internal void UpdateReport(ReportModel reportModel)
        {
            ReportModel = reportModel;
            OnPropertyChanged(nameof(Tittle));
        }
    }
}
