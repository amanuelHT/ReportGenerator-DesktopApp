using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;

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
                // This query assumes your ReportImageModels table or set has a foreign key to the report table via 'ReportModelId'
                var imageDtos = await context.ReportImageModels
                    .AsNoTracking()
                    .Where(img => img.ReportModelId == reportId)
                    .ToListAsync();

                var imageModels = new List<ReportImageModel>();
                foreach (var imgDto in imageDtos)
                {
                    imageModels.Add(new ReportImageModel(imgDto.Id, imgDto.Name, imgDto.ImageUrl));
                }

                return imageModels;
            }
        }
    }
}
