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

        // Retrieve  full report data of the report
        (
             ReportModel reportData, TestUtførtAvModel testUtførtAvModel, KontrollertAvførtAvModel KontrollertAvførtAvModel,
            List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels,
            List<ReportImageModel> images,
            List<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels,
            List<ConcreteDensityModel> concreteDensityModels,
            List<TrykktestingModel> trykktestingModels,
            List<TestModel> tests,
            List<verktøyModel> verktøyer
            ) = await _reportStore.GetReportData(_reportid);

        if (reportData == null)
        {
            return;
        }


        reportData.Images = images;
        reportData.Test = tests;
        reportData.Verktøy = verktøyer;

        reportData.TestUtførtAvModel = testUtførtAvModel;

        reportData.KontrollertAvførtAvModel = KontrollertAvførtAvModel;

        reportData.DataFraOppdragsgiverPrøver = dataFraOppdragsgiverPrøverModels;

        reportData.DataEtterKuttingOgSlipingModel = dataEtterKuttingOgSlipingModels;

        reportData.ConcreteDensityModel = concreteDensityModels;

        reportData.TrykktestingModel = trykktestingModels;


        EditReportVM editReportVM = new EditReportVM(reportData, _reportStore, _modalNavigation, _navigationStore, _modalNavigation);


        _modalNavigation.ShowReportWindow(new EditReportView { DataContext = editReportVM }, "Edit Report");





    }
}
