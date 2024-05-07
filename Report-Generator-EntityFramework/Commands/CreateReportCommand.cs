using Domain.Models;
using Report_Generator_Domain.Commands;

namespace Report_Generator_EntityFramework.Commands
{
    public class CreateReportCommand : ICreateReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public CreateReportCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(ReportModel reportModel)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                context.ReportModels.Add(reportModel);


                // Save changes to the database
                await context.SaveChangesAsync();
            }
        }
    }
}