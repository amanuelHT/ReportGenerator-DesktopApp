using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Queries;
using System.Diagnostics;

namespace Report_Generator_EntityFramework.Queries
{
    public class GetAllReportsQuery : IGetAllReportsQuery
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public GetAllReportsQuery(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<IEnumerable<ReportModel>> Execute()
        {
            try
            {
                using (var context = _contextFactory.Create())
                {
                    var reports = await context.ReportModels
                        .Include(r => r.Images)
                        .ToListAsync();

                    return reports.Select(report => new ReportModel(
                        report.Id,
                        report.Tittle,
                        report.Status,
                        report.Kunde,
                        report.AvvikFraStandarder,
                        report.MotattDato,
                        report.Kommentarer



                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // Log the exception
                return Enumerable.Empty<ReportModel>(); // Return an empty collection or handle as needed
            }
        }
    }
}
