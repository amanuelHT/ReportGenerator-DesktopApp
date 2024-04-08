using Domain.Models; // Import the Domain Models namespace
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.ReportsDbContext;

namespace Report_Generator_EntityFramework.Commands
{
    public class GetReportDataCommand : IGetReportDataCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public GetReportDataCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ReportModel> Execute(Guid reportId)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                var reportModel = await context.ReportModels
                    .Include(report => report.Images)
                    .FirstOrDefaultAsync(report => report.Id == reportId);

                if (reportModel == null)
                    return null;

                return new ReportModel(
                    reportModel.Id,
                    reportModel.Tittle, // Corrected spelling here
                    reportModel.Status,
                    reportModel.Kunde
                );
            }
        }
    }
}
