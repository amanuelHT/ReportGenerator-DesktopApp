using Domain.Models;
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
                    .FindAsync(reportId);

                if (reportModelDto == null) return null;

                return new ReportModel(
                    reportModelDto.Id,
                    reportModelDto.Tittle,
                    reportModelDto.Status,
                    reportModelDto.Kunde);
            }
        }
    }
}
