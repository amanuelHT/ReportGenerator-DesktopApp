using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using Final_project.ViewModels;
using Final_project.Views;
using Report_Generator_Domain.Models;

public class OpenEditCommand : CommandBase
{
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigation _modalNavigation;
    private readonly ReportStore _reportStore;
    private readonly Guid _reportid;

    public OpenEditCommand(Guid reportid, ReportStore reportStore, NavigationStore navigationStore, ModalNavigation modalNavigation)
    {
        _navigationStore = navigationStore;
        _modalNavigation = modalNavigation;
        _reportStore = reportStore;
        _reportid = reportid;


    }

    public override async void Execute(object parameter)
    {

        // Retrieve the full report data of the report
        (ReportModel reportData,
            List<ReportImageModel> images,
            List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels,
            List<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels,
            List<ConcreteDensityModel> concreteDensityModels,
            List<TrykktestingModel> trykktestingModels
            ) = await _reportStore.GetReportData(_reportid);

        if (reportData == null)
        {
            return;
        }


        reportData.Images = images;

        reportData.DataFraOppdragsgiverPrøver = dataFraOppdragsgiverPrøverModels;

        reportData.DataEtterKuttingOgSlipingModel = dataEtterKuttingOgSlipingModels;

        reportData.ConcreteDensityModel = concreteDensityModels;

        reportData.TrykktestingModel = trykktestingModels;


        // Create and navigate to the edit report view model
        EditReportVM editReportVM = new EditReportVM(reportData, _reportStore, _modalNavigation, _navigationStore, _modalNavigation);


        _modalNavigation.ShowReportWindow(new EditReportView { DataContext = editReportVM }, "Edit Report");





    }
}
