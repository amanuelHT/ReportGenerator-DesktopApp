using Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface IGetImageForReportCommand
    {
        Task<List<ReportImageModel>> Execute(Guid reportId);
    }
}
