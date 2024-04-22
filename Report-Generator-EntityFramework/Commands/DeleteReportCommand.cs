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
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                // Find the report model by its ID
                var reportModel = await context.ReportModels
                    .Include(report => report.Images) // Include related images
                    .FirstOrDefaultAsync(report => report.Id == id);

                if (reportModel != null)
                {
                    // Remove all associated images
                    context.ReportImageModels.RemoveRange(reportModel.Images);

                    // Remove the report model itself
                    context.ReportModels.Remove(reportModel);

                    // Save changes to the database
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
