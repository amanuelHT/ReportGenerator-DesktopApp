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
                var images = await context.ReportImageModels
                    .Where(image => image.ReportModelId == reportId)
                    .ToListAsync();

                return images;
            }
        }
    }
}
