using Domain.Models;
using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.DTOs;

namespace Report_Generator_EntityFramework.Commands
{
    public class UpdateReportCommand : IUpdateReportCommand
    {

        private readonly ReportModelDbContextFactory _contextFactory;

        public UpdateReportCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(ReportModel reportModel, List<ReportImageModel> images)
        {

            using (ReportModelDbContext context = _contextFactory.Create())
            {
                ReportModelDto reportModelDto = new ReportModelDto()
                {
                    Id = reportModel.Id,
                    Tittle = reportModel.Tittle,
                    Status = reportModel.Status,
                    Kunde = reportModel.Kunde,
                    Images = new List<ReportImageModelDto>()
                };


                foreach (var image in images)
                {
                    var imageDto = new ReportImageModelDto
                    {

                        Name = image.Name,
                        ImageUrl = image.ImageUrl
                    };
                    reportModelDto.Images.Add(imageDto);
                }

                context.ReportModels.Update(reportModelDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
