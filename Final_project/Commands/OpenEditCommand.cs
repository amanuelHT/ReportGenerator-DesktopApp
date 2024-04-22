using Domain.Models;
using Final_project.Commands;
using Final_project.Components;
using Final_project.Stores;
using Final_project.ViewModels;
using Final_project.Views;
using Report_Generator_Domain.Models;

public class OpenEditCommand : CommandBase
{
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigation _modalNavigation;
    private readonly ReportStore _reportStore;

    private readonly ReportListingItemVM _reportListingItemVM;

    public OpenEditCommand(ReportListingItemVM reportListingItemVM, ReportStore reportStore, NavigationStore navigationStore, ModalNavigation modalNavigation)
    {
        _navigationStore = navigationStore;
        _modalNavigation = modalNavigation;
        _reportStore = reportStore;

        _reportListingItemVM = reportListingItemVM;
    }

    public override async void Execute(object parameter)
    {
        Guid reportId = _reportListingItemVM.ReportModel.Id;

        // Retrieve the full report data with images
        (ReportModel reportData,
            List<ReportImageModel> images,
            List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels,
            List<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels,
            List<ConcreteDensityModel> concreteDensityModels,
            List<TrykktestingModel> trykktestingModels
            ) = await _reportStore.GetReportData(reportId);

        if (reportData == null)
        {
            // Handle the case where the report is not found or there's an error loading it.
            return;
        }

        // Add retrieved datas to the report data
        reportData.Images = images;

        reportData.DataFraOppdragsgiverPrøver = dataFraOppdragsgiverPrøverModels;

        reportData.DataEtterKuttingOgSlipingModel = dataEtterKuttingOgSlipingModels;

        reportData.ConcreteDensityModel = concreteDensityModels;

        reportData.TrykktestingModel = trykktestingModels;


        // Create and navigate to the edit report view model
        EditReportVM editReportVM = new EditReportVM(reportData, _reportStore, _modalNavigation, _navigationStore);
        // Show the window
        ReportWindowHelper.ShowReportWindow(new EditReportView { DataContext = editReportVM }, "Edit Report");





    }
}
