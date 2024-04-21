using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;


namespace Report_Generator_EntityFramework.Commands
{
    public class UpdateReportCommand : IUpdateReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;
        private readonly ICreateImageCommand _createImageCommand;

        public UpdateReportCommand(ReportModelDbContextFactory contextFactory, ICreateImageCommand createImageCommand)
        {
            _contextFactory = contextFactory;
            _createImageCommand = createImageCommand;
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

                    // Filter out images that already exist in the database
                    var newImages = reportModel.Images
                        .Where(newImage => existingReport.Images.All(existingImage => existingImage.Id != newImage.Id))
                        .ToList();

                    // Add new filtered images to the report
                    await _createImageCommand.Execute(reportModel.Id, newImages);

                    // Save changes to the context
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
