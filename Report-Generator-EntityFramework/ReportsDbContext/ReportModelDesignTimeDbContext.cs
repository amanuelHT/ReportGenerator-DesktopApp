using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Report_Generator_EntityFramework.ReportsDbContext
{
    public class ReportModelDesignTimeDbContext : IDesignTimeDbContextFactory<ReportModelDbContext>
    {


        public ReportModelDbContext CreateDbContext(string[] args = null)
        {

            return new ReportModelDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source =MyReports.db").Options);

        }
    }
}
