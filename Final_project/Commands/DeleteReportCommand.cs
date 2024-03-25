using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{
    public class DeleteReportCommand : AsyncCommandBase
    {
        private readonly ReportListingItemVM _reportListingItemVM;
        private readonly ReportStore _reportStore;

        public DeleteReportCommand(ReportListingItemVM reportListingItemVM, ReportStore reportStore)
        {
            _reportListingItemVM = reportListingItemVM;
            _reportStore = reportStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _reportListingItemVM.ErrorMessage = null;
            _reportListingItemVM.IsDeleting = true;

            ReportModel reportModel = _reportListingItemVM.ReportModel;


            try
            {
                await _reportStore.Delete(reportModel.Id);
            }
            catch (Exception)
            {
                _reportListingItemVM.ErrorMessage = "Failed to delete YouTube viewer. Please try again later.";
            }
            finally
            {
                _reportListingItemVM.IsDeleting = false;
            }
        }
    }

}
