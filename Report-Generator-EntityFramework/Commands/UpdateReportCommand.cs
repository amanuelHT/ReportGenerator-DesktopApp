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

        public async Task Execute(ReportModel reportModel)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                // Convert ReportModel to DTO
                ReportModelDto reportModelDto = new ReportModelDto()
                {
                    Id = reportModel.Id,
                    Tittle = reportModel.Tittle,
                    Status = reportModel.Status,
                    Kunde = reportModel.Kunde,
                    Images = new List<ReportImageModelDto>()

                };

<<<<<<< HEAD


=======
>>>>>>> 97a6e3179d9a800a68fc242a1e71c9c37fb6aee6
                foreach (var image in reportModel.Images)
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



