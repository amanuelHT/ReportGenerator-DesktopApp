using Domain.Models;

namespace Report_Generator_Domain.Commands
{
    public interface ICreateReportCommand
    {
        Task Execute(ReportModel reportModel);
    }
}
