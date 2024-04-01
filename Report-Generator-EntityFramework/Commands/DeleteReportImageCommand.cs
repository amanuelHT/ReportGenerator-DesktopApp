using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.DTOs;

namespace Report_Generator_EntityFramework.Commands
{
    public class DeleteReportImageCommand : IDeleteReportImageCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public DeleteReportImageCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                ReportImageModelDto reportImageDto = new ReportImageModelDto()
                {
                    Id = id,
                };

                context.ReportImageModels.Remove(reportImageDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
