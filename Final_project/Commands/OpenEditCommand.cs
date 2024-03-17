


using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{
    public class OpenEditCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        //  private readonly ReportModel _reportModel;
        private readonly ReportStore _reportStore;
        private readonly ReportListingItemVM _reportListingItemVM;

        public OpenEditCommand(ReportListingItemVM reportListingItemVM, ReportStore reportStore, NavigationStore navigationStore)
        {


            _navigationStore = navigationStore;
            _reportStore = reportStore;
            _reportListingItemVM = reportListingItemVM;


        }

        public override void Execute(object parameter)
        {
            ReportModel reportModel = _reportListingItemVM.ReportModel;

            EditReportVM editReportVM = new EditReportVM(reportModel, _reportStore, _navigationStore);
            _navigationStore.CurrentView = editReportVM;
        }
    }
}
