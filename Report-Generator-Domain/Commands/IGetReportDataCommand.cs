using Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface IGetReportDataCommand
    {
        Task<(ReportModel report, List<ReportImageModel> images)> Execute(Guid reportId);
    }
}
