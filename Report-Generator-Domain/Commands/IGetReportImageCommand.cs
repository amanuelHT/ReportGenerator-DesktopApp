using Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface IGetReportImageCommand
    {
        Task<List<ReportImageModel>> Execute(Guid reportId);
    }
}
