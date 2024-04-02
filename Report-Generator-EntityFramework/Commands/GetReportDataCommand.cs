using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;

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
                // Query the report including its associated images
                var reportModelDto = await context.ReportModels
                    .Include(report => report.Images) // Include related images
                    .FirstOrDefaultAsync(report => report.Id == reportId);

                if (reportModelDto == null)
                    return null;

                // Convert DTO images to domain model
                var reportImages = reportModelDto.Images.Select(imageDto =>
                    new ReportImageModel(
                        imageDto.Id,
                        imageDto.Name,
                        imageDto.ImageUrl))
                    .ToList();

                // Create the ReportModel object
                var reportModel = new ReportModel(
                    reportModelDto.Id,
                    reportModelDto.Tittle,
                    reportModelDto.Status,
                    reportModelDto.Kunde,
                    reportImages

                    )
                {

                };

                return reportModel;
            }
        }
    }
}
