using Domain.Models; // Ensure this namespace contains all your domain models
using Microsoft.EntityFrameworkCore; // Necessary for EF Core operations like Include
using Report_Generator_Domain.Commands;
using Report_Generator_Domain.Models;

namespace Report_Generator_EntityFramework.Commands
{
    public class GetReportDataCommand : IGetReportDataCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public GetReportDataCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<(
             ReportModel report, DataFraOppdragsgiverPrøverModel DataFraOppdragsgiverPrøverModel,
            List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels,
            List<ReportImageModel> images,
            List<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels,
            List<ConcreteDensityModel> concreteDensityModels,
            List<TrykktestingModel> trykktestingModels,
            List<TestModel> tests,
            List<verktøyModel> verktøyer
            )> Execute(Guid reportId)
        {
            using (var context = _contextFactory.Create())
            {
                var report = await context.ReportModels
                    .Include(r => r.Images)
                    .Include(r => r.Verktøy)
                    .Include(r => r.Test)
                    .Include(r => r.DataFraOppdragsgiverPrøver)
                       .ThenInclude(s => s.ConcreteDensityModel)
                    .Include(r => r.DataFraOppdragsgiverPrøver)
                       .ThenInclude(s => s.TrykktestingModel)
                    .Include(r => r.DataFraOppdragsgiverPrøver)
                       .ThenInclude(s => s.DataEtterKuttingOgSlipingModel)

                       .FirstOrDefaultAsync(r => r.Id == reportId);

                if (report == null)
                    return (null, null, null, null, null, null, null, null, null);

                var images = report.Images.ToList();
                var tests = report.Test.ToList();
                var verktøies = report.Verktøy.ToList();
                var dataFraOppdragsgiverPrøverModels = report.DataFraOppdragsgiverPrøver.ToList();

                var concreteDensityModels = dataFraOppdragsgiverPrøverModels.SelectMany(d => d.ConcreteDensityModel).ToList();
                var trykktestingModels = dataFraOppdragsgiverPrøverModels.SelectMany(t => t.TrykktestingModel).ToList();
                var dataEtterKuttingOgSlipingModels = dataFraOppdragsgiverPrøverModels.SelectMany(d => d.DataEtterKuttingOgSlipingModel).ToList();

                var dataFraOppdragsgiverPrøverModel = dataFraOppdragsgiverPrøverModels.FirstOrDefault();

                return (
                    report,
                    dataFraOppdragsgiverPrøverModel,
                    dataFraOppdragsgiverPrøverModels,
                    images,
                    dataEtterKuttingOgSlipingModels,
                    concreteDensityModels,
                    trykktestingModels,
                    tests,
                    verktøies

                );
            }
        }
    }
}
