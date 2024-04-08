using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.ReportsDbContext;

namespace Report_Generator_EntityFramework.Commands
{
    public class GetReportImageCommand : IGetReportImageCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public GetReportImageCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<ReportImageModel>> Execute(Guid reportId)
        {
            using (var context = _contextFactory.Create())
            {
                // Query the images associated with the specified reportId
                var imageDtos = await context.ReportImageModels
                    .AsNoTracking()
                    .Where(img => img.ReportModelId == reportId)
                    .ToListAsync();

                // Convert imageDtos to ReportImageModel instances
                var imageModels = imageDtos.Select(imgDto =>
                    new ReportImageModel(imgDto.Id, imgDto.Name, imgDto.ImageUrl, reportId)) // Pass reportId as reportModelId
                    .ToList();

                return imageModels;
            }
        }
    }
}
