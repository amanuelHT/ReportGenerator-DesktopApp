using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Queries;
using Report_Generator_EntityFramework.DTOs;

namespace Report_Generator_EntityFramework.Queries
{
    public class GetAllReportsQuery : IGetAllReportsQuery
    {

        private readonly ReportModelDbContextFactory _contextFactory;

        public GetAllReportsQuery(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<ReportModel>> Execute()
        {

            using (ReportModelDbContext context = _contextFactory.Create())
            {
                IEnumerable<ReportModelDto> reportmodeldtos = await context.ReportModels.ToListAsync();

                return reportmodeldtos.Select(y => new ReportModel(y.Id, y.Tittle, y.Status, y.Kunde));
            }


        }
    }
}
