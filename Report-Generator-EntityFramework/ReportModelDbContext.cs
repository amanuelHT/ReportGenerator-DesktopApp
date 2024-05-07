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
        public DbSet<TestUtførtAvModel> TestUtførtAvModels { get; set; }
        public DbSet<KontrollertAvførtAvModel> KontrollertAvførtAvModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ReportModel>()
                .HasOne(report => report.TestUtførtAvModel)  // Change to TestUtførtAvModel
                .WithOne(test => test.Report)                // Assuming Report is the navigation property in TestUtførtAvModel pointing back to ReportModel
                .HasForeignKey<TestUtførtAvModel>(test => test.ReportModelID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReportModel>()
                .HasOne(report => report.KontrollertAvførtAvModel)
                .WithOne(kontrollertAv => kontrollertAv.Report)
                .HasForeignKey<KontrollertAvførtAvModel>(kontrollertAv => kontrollertAv.ReportModelID)
                .OnDelete(DeleteBehavior.Cascade);



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

            modelBuilder.Entity<DataEtterKuttingOgSlipingModel>()
                       .HasOne(prøver => prøver.ReportModel)
                       .WithMany(table => table.DataEtterKuttingOgSlipingModel)
                       .HasForeignKey(prøver => prøver.ReportModelId)
                       .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ConcreteDensityModel>()
               .HasOne(prøver => prøver.ReportModel)
               .WithMany(table => table.ConcreteDensityModel)
               .HasForeignKey(prøver => prøver.ReportModelId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrykktestingModel>()
               .HasOne(prøver => prøver.ReportModel)
               .WithMany(table => table.TrykktestingModel)
               .HasForeignKey(prøver => prøver.ReportModelId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}







