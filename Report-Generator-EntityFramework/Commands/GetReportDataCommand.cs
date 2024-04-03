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
                var reportModelDto = await context.ReportModels
                    .Include(report => report.Images)
                    .FirstOrDefaultAsync(report => report.Id == reportId);

                if (reportModelDto == null)
                    return null;

                var reportImages = reportModelDto.Images
                    .Select(imageDto => new ReportImageModel(
                        imageDto.Id,
                        imageDto.Name ?? "DefaultName", // Handle NULL Name
                        imageDto.ImageUrl ?? "DefaultImageUrl")) // Handle NULL ImageUrl
                    .ToList();

                return new ReportModel(
                    reportModelDto.Id,
                    reportModelDto.Tittle,
                    reportModelDto.Status,
                    reportModelDto.Kunde,
                    reportImages
                );
            }
        }

    }

}
