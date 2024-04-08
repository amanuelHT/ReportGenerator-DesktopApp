using Microsoft.EntityFrameworkCore;

namespace Report_Generator_EntityFramework.ReportsDbContext
{
    public class ReportModelDbContextFactory
    {

        private readonly DbContextOptions _options;

        public ReportModelDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public ReportModelDbContext Create()
        {

            return new ReportModelDbContext(_options);

        }
    }
}
