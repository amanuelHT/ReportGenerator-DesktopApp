using Domain.Models;
using Report_Generator_Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface IGetReportDataCommand
    {
        Task<(
            ReportModel report, DataFraOppdragsgiverPrøverModel DataFraOppdragsgiverPrøverModel,
            List<ReportImageModel> images,
            List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels,
            List<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels,
             List<ConcreteDensityModel> concreteDensityModels,
            List<TrykktestingModel> trykktestingModels
            )> Execute(Guid reportId);
    }
}
