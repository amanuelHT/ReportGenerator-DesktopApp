using Microsoft.EntityFrameworkCore;
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

        public async Task Execute(Guid imageId)
        {
            using (var context = _contextFactory.Create())
            {
                // Find the report that contains the image
                var reportWithImage = await context.ReportModels
                    .Include(r => r.Images)
                    .FirstOrDefaultAsync(r => r.Images.Any(img => img.Id == imageId));

                if (reportWithImage != null)
                {
                    // Remove the image from the report's collection
                    var imageToRemove = reportWithImage.Images.FirstOrDefault(img => img.Id == imageId);
                    if (imageToRemove != null)
                    {
                        reportWithImage.Images.Remove(imageToRemove);

                        // Save changes to the context
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
