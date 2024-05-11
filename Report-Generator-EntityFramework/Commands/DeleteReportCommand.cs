using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;

namespace Report_Generator_EntityFramework.Commands
{
    public class DeleteReportCommand : IDeleteReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public DeleteReportCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (var context = _contextFactory.Create())
            {
                var reportModel = await context.ReportModels
                    .FirstOrDefaultAsync(report => report.Id == id);

                if (reportModel != null)
                {
                    context.ReportModels.Remove(reportModel);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
