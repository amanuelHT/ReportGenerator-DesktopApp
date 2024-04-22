using Report_Generator_Domain.ITables;
using Report_Generator_Domain.Models; // Import the namespace containing the interface

namespace Report_Generator_EntityFramework.Tables
{
    // Implement the interface ICreateDataFraOppdragsgiverPrøverModelCommand
    public class CreateDataFraOppdragsgiverPrøverModelCommand : ICreateDataFraOppdragsgiverPrøverModelCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public CreateDataFraOppdragsgiverPrøverModelCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid tableid, List<DataFraOppdragsgiverPrøverModel> reportprøver)
        {
            //using (var context = _contextFactory.Create())
            //{
            //    var table = await context.DataFraOppdragsgiverTableModels.FindAsync(tableid);
            //    if (table == null)
            //    {
            //        throw new ArgumentException("table not found.", nameof(tableid));
            //    }

            //    //foreach (var prøver in reportprøver)
            //    //{
            //    //    prøver.TableModelId = tableid;
            //    //    context.DataFraOppdragsgiverPrøverModels.Add(prøver);
            //    //}

            //    await context.SaveChangesAsync();
            //}
        }
    }
}
