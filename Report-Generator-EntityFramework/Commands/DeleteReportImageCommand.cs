using Report_Generator_Domain.Commands;

namespace Report_Generator_EntityFramework.Commands
{
    public class DeleteReportImageCommand : IDeleteReportImageCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public DeleteReportImageCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // In DeleteReportImageCommand
        public async Task Execute(Guid id)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                var reportImageDto = await context.ReportImageModels.FindAsync(id);
                if (reportImageDto != null)
                {
                    context.ReportImageModels.Remove(reportImageDto);
                    await context.SaveChangesAsync();
                }
            }
        }

    }
}
