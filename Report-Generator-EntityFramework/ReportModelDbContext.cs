using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Models;


namespace Report_Generator_EntityFramework
{


    public class ReportModelDbContext : DbContext
    {
        public ReportModelDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<ReportModel> ReportModels { get; set; }
        public DbSet<ReportImageModel> ReportImageModels { get; set; }
        public DbSet<DataFraOppdragsgiverPrøverModel> DataFraOppdragsgiverPrøverModels { get; set; }
        public DbSet<DataEtterKuttingOgSlipingModel> DataEtterKuttingOgSlipingModels { get; set; }
        public DbSet<ConcreteDensityModel> concreteDensityModels { get; set; }
        public DbSet<TrykktestingModel> trykktestingModels { get; set; }
        public DbSet<TestModel> tests { get; set; }
        public DbSet<verktøyModel> verktøies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define foreign key relationship between ReportModel and ReportImageModel
            modelBuilder.Entity<ReportImageModel>()
                .HasOne(image => image.ReportModel)
                .WithMany(report => report.Images)
                .HasForeignKey(image => image.ReportModelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestModel>()
                .HasOne(test => test.ReportModel)
                .WithMany(report => report.Test)
                .HasForeignKey(test => test.ReportModelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<verktøyModel>()
                .HasOne(verktøy => verktøy.ReportModel)
                .WithMany(report => report.Verktøy)
                .HasForeignKey(verktøy => verktøy.ReportModelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define foreign key relationship between DataFraOppdragsgiverPrøverModel and DataFraOppdragsgiverTableModel
            modelBuilder.Entity<DataFraOppdragsgiverPrøverModel>()
                .HasOne(prøver => prøver.ReportModel)
                .WithMany(table => table.DataFraOppdragsgiverPrøver)
                .HasForeignKey(prøver => prøver.ReportModelId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ConcreteDensityModel>()
               .HasOne(prøver => prøver.DataFraOppdragsgiverPrøverModel)
               .WithMany(table => table.ConcreteDensityModel)
               .HasForeignKey(prøver => prøver.DataFraOpdragsgiverId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrykktestingModel>()
               .HasOne(prøver => prøver.DataFraOppdragsgiverPrøverModel)
               .WithMany(table => table.TrykktestingModel)
               .HasForeignKey(prøver => prøver.DataFraOpdragsgiverId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DataEtterKuttingOgSlipingModel>()
               .HasOne(prøver => prøver.DataFraOppdragsgiverPrøverModel)
               .WithMany(table => table.DataEtterKuttingOgSlipingModel)
               .HasForeignKey(prøver => prøver.DataFraOpdragsgiverId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}







