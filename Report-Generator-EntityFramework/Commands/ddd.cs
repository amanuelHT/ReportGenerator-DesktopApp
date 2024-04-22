//using Domain.Models;
//using Microsoft.EntityFrameworkCore;
//using Report_Generator_Domain.Commands;
//using Report_Generator_EntityFramework.ReportsDbContext;

//namespace Report_Generator_EntityFramework.Commands
//{
//    public class GetImageForReportCommand : IGetImageForReportCommand
//    {
//        private readonly ReportModelDbContextFactory _contextFactory;

//        public GetImageForReportCommand(ReportModelDbContextFactory contextFactory)
//        {
//            _contextFactory = contextFactory;
//        }

//        public async Task<List<ReportImageModel>> Execute(Guid reportId)
//        {
//            using (var context = _contextFactory.Create())
//            {
//                var reportImages = await context.ReportImageModels
//                    .Where(image => image.ReportModelId == reportId)
//                    .ToListAsync();

//                return reportImages;
//            }
//        }
//    }
//}
