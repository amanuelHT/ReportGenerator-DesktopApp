using Report_Generator_Domain.Commands;
using Report_Generator_EntityFramework.DTOs;

namespace Report_Generator_EntityFramework.Commands
{
    public class DeleteReportCommand : IDeleteReportCommand
    {

        private readonly ReportModelDbContextFactory _contextFactory;

        public DeleteReportCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                ReportModelDto reportModelDto = new ReportModelDto()
                {
                    Id = id,

                };

                context.ReportModels.Remove(reportModelDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
