using Domain.Models;
using Report_Generator_Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface IGetReportDataCommand
    {
        Task<(
             ReportModel report, DataFraOppdragsgiverPrøverModel DataFraOppdragsgiverPrøverModel,
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
