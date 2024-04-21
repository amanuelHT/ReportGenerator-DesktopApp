using Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<(ReportModel report, List<ReportImageModel> images, List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels)> Execute(Guid reportId)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                var report = await context.ReportModels
                    .FirstOrDefaultAsync(report => report.Id == reportId);

                if (report == null)
                    return (null, null, null);

                var images = await context.ReportImageModels
                    .Where(image => image.ReportModelId == reportId)
                    .ToListAsync();

                var prøve = await context.DataFraOppdragsgiverPrøverModels
                    .Where(image => image.ReportModelId == reportId)
                    .ToListAsync();

                return (report, images, prøve);
            }
        }
    }
}
