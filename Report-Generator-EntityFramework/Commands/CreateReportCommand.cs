using Domain.Models; // Import the Domain Models namespace
using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.DTOs;

namespace Report_Generator_EntityFramework.Commands
{
    public class CreateReportCommand : ICreateReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public CreateReportCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(ReportModel reportModel)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                // Create a new ReportModelDto and populate its properties
                ReportModelDto reportModelDto = new ReportModelDto()
                {
                    Id = reportModel.Id,
                    Tittle = reportModel.Tittle,
                    Status = reportModel.Status,
                    Kunde = reportModel.Kunde,
                    Images = new List<ReportImageModelDto>() // Initialize the Images collection
                };

                // Iterate over the images in the ReportModel and map them to ReportImageModelDto
                foreach (var image in reportModel.Images)
                {
                    // Create a new ReportImageModelDto and populate its properties
                    var imageDto = new ReportImageModelDto
                    {
                        Name = image.Name,
                        ImageUrl = image.ImageUrl
                    };

                    // Add the imageDto to the Images collection of reportModelDto
                    reportModelDto.Images.Add(imageDto);
                }

                // Add the reportModelDto to the DbSet and save changes to the database
                context.ReportModels.Add(reportModelDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
