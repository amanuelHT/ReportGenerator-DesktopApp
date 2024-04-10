using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.ReportsDbContext;

namespace Report_Generator_EntityFramework.Commands
{
    public class UpdateReportCommand : IUpdateReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;
        private readonly ICreateImageCommand _createImageCommand;
        private readonly IDeleteReportImageCommand _deleteReportImageCommand;

        public UpdateReportCommand(ReportModelDbContextFactory contextFactory, ICreateImageCommand createImageCommand, IDeleteReportImageCommand deleteReportImageCommand)
        {
            _contextFactory = contextFactory;
            _createImageCommand = createImageCommand;
            _deleteReportImageCommand = deleteReportImageCommand;
        }

        public async Task Execute(ReportModel reportModel)
        {
            using (var context = _contextFactory.Create())
            {
                var existingReport = await context.ReportModels
                    .Include(r => r.Images) // Include images for updating
                    .FirstOrDefaultAsync(r => r.Id == reportModel.Id);

                if (existingReport != null)
                {
                    existingReport.Tittle = reportModel.Tittle;
                    existingReport.Status = reportModel.Status;
                    existingReport.Kunde = reportModel.Kunde;

                    // Delete all existing images related to the report
                    var existingImages = await context.ReportImageModels.Where(i => i.ReportModelId == reportModel.Id).ToListAsync();
                    foreach (var existingImage in existingImages)
                    {
                        await _deleteReportImageCommand.Execute(existingImage.Id);
                    }

                    // Add new images to the report
                    await _createImageCommand.Execute(reportModel.Id, reportModel.Images);

                    // Save changes to the context
                    await context.SaveChangesAsync();
                }

                // Save changes to the context
                await context.SaveChangesAsync();
            }
        }
    }
}

