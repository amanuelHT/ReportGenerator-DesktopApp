//using Domain.Models;
//using Report_Generator_Domain.Commands;
//using Report_Generator_EntityFramework.DTOs;

//namespace Report_Generator_EntityFramework.Commands
//{
//    public class CreateImageCommand : ICreateImageCommand
//    {
//        private readonly ReportModelDbContextFactory _contextFactory;

//        public CreateImageCommand(ReportModelDbContextFactory contextFactory)
//        {
//            _contextFactory = contextFactory;
//        }

//        public async Task Execute(Guid reportId, List<ReportImageModel> images)
//        {
//            using (ReportModelDbContext context = _contextFactory.Create())
//            {
//                foreach (var image in images)
//                {
//                    ReportImageModelDto imageDto = new ReportImageModelDto
//                    {
//                        ReportModelId = reportId,
//                        Id = image.Id,
//                        Name = image.Name,
//                        ImageUrl = image.ImageUrl
//                    };

//                    context.ReportImageModels.Add(imageDto);
//                }

//                await context.SaveChangesAsync();
//            }
//        }
//    }
//}