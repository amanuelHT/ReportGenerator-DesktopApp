// DeleteReportImageCommand.cs
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.ReportsDbContext;

namespace Report_Generator_EntityFramework.Commands
{
    public class ImageDeletionCommand : IDeleteReportImageCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public ImageDeletionCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid imageId)
        {
            using (var context = _contextFactory.Create())
            {
                var reportWithImage = await context.ReportModels
                    .Include(r => r.Images)
                    .FirstOrDefaultAsync(r => r.Images.Any(img => img.Id == imageId));

                if (reportWithImage != null)
                {
                    var imageToRemove = reportWithImage.Images.FirstOrDefault(img => img.Id == imageId);
                    if (imageToRemove != null)
                    {
                        reportWithImage.Images.Remove(imageToRemove);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
