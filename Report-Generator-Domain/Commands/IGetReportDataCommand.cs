using Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface IGetReportDataCommand
    {
        Task<ReportModel> Execute(Guid reportId);
    }


}
