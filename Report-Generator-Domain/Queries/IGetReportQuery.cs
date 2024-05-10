using Domain.Models;
using Report_Generator_Domain.Models;

namespace Report_Generator_Domain.Queries
{
    public interface IGetReportQuery
    {
        Task<(
        ReportModel report, TestUtførtAvModel testUtførtAvModel, KontrollertAvførtAvModel KontrollertAvførtAvModel,
             List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels,
            List<ReportImageModel> images,
            List<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels,
            List<ConcreteDensityModel> concreteDensityModels,
            List<TrykktestingModel> trykktestingModels,
            List<TestModel> tests,
            List<verktøyModel> verktøyer
            )> Execute(Guid reportId);
    }
}