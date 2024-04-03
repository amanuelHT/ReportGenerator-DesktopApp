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
        public DbSet<ReportImageModelDto> ReportImageModels { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define foreign key relationship between ReportImageModelDto and ReportModelDto
            modelBuilder.Entity<ReportImageModelDto>()
                .HasOne(image => image.ReportModel)
                .WithMany(report => report.Images)
                .HasForeignKey(image => image.ReportModelId)
                .OnDelete(DeleteBehavior.Cascade); // Optionally specify cascade delete behavior
        }

    }
}




