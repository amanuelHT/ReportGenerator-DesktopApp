using Domain.Models;

namespace Report_Generator_Domain.Queries
{
    public interface IGetReportQuery
    {
        Task<IEnumerable<ReportModel>> Execute();
    }
}
