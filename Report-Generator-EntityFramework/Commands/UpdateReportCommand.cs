using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.ReportsDbContext;

namespace Report_Generator_EntityFramework.Commands
{
    public class UpdateReportCommand : IUpdateReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public UpdateReportCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(ReportModel reportModel)
        {
            using (var context = _contextFactory.Create())
            {
                var existingReport = await context.ReportModels
                    .Include(r => r.Images)
                    .FirstOrDefaultAsync(r => r.Id == reportModel.Id);

                if (existingReport != null)
                {
                    existingReport.Tittle = reportModel.Tittle; // Corrected spelling here
                    existingReport.Status = reportModel.Status;
                    existingReport.Kunde = reportModel.Kunde;

                    // Update existing images and add new images
                    foreach (var image in reportModel.Images)
                    {
                        var existingImage = existingReport.Images
                            .FirstOrDefault(i => i.Id == image.Id);

                        if (existingImage != null)
                        {
                            existingImage.Name = image.Name;
                            existingImage.ImageUrl = image.ImageUrl;
                        }
                        else
                        {
                            // Add new image
                            existingReport.Images.Add(image);
                        }
                    }

                    // Remove any images not present in the updated report
                    existingReport.Images.RemoveAll(i => !reportModel.Images.Select(img => img.Id).Contains(i.Id));

                    context.ReportModels.Update(existingReport);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
