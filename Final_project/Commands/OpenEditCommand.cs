using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using Final_project.ViewModels;

public class OpenEditCommand : CommandBase
{
    private readonly ModalNavigation _navigationStore;
    private readonly ReportStore _reportStore;
    private readonly ReportListingItemVM _reportListingItemVM;

    public OpenEditCommand(ReportListingItemVM reportListingItemVM, ReportStore reportStore, ModalNavigation navigationStore)
    {
        _navigationStore = navigationStore;
        _reportStore = reportStore;
        _reportListingItemVM = reportListingItemVM;
    }

    public override async void Execute(object parameter)
    {
        Guid reportId = _reportListingItemVM.ReportModel.Id;

        // Retrieve the full report data with images
        ReportModel reportData = await _reportStore.GetReportData(reportId);

        if (reportData == null)
        {
            // Handle the case where the report is not found or there's an error loading it.
            return;
        }

        // Create the edit report view model with the full report data
        EditReportVM editReportVM = new EditReportVM(reportData, _reportStore, _navigationStore);

        // Change the current view to the edit report view
        _navigationStore.CurrentView = editReportVM;
    }
}
