using Microsoft.EntityFrameworkCore;
using Report_Generator_EntityFramework.DTOs;

namespace Report_Generator_EntityFramework
{


    public class ReportModelDbContext : DbContext
    {
        public ReportModelDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ReportModelDto> ReportModels { get; set; }
    }
}




