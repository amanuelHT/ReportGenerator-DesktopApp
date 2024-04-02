using Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface IUpdateReportCommand
    {
        Task Execute(ReportModel reportModel);
    }
}
