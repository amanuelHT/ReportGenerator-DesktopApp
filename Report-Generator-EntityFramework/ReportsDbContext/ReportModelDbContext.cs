using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Report_Generator_EntityFramework.ReportsDbContext
{


    public class ReportModelDbContext : DbContext
    {



        public ReportModelDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet properties for your models
        public DbSet<ReportModel> ReportModels { get; set; }
        public DbSet<ReportImageModel> ReportImageModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define foreign key relationship between ReportModel and ReportImageModel
            modelBuilder.Entity<ReportImageModel>()
                .HasOne(image => image.ReportModel) // Each ReportImageModel belongs to one ReportModel
                .WithMany(report => report.Images)  // Each ReportModel can have multiple ReportImageModels
                .HasForeignKey(image => image.ReportModelId) // Foreign key property in ReportImageModel
                .OnDelete(DeleteBehavior.Cascade); // Optionally specify cascade delete behavior
        }
    }
}




