using Domain.Models;

namespace Report_Generator_Domain.Queries
{
    public interface IGetAllReportsQuery
    {
        Task<IEnumerable<ReportModel>> Execute();
    }
}
