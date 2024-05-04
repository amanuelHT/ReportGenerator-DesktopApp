using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;
using Report_Generator_Domain.Models;

namespace Report_Generator_EntityFramework.Commands
{
    public class UpdateReportCommand : IUpdateReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public UpdateReportCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;

        }

        public async Task Execute(ReportModel reportModel)
        {
            using (var context = _contextFactory.Create())
            {
                var existingReport = await context.ReportModels
                    .Include(r => r.Images) // Include images for updating
                    .Include(r => r.DataFraOppdragsgiverPrøver)
                     .ThenInclude(p => p.TrykktestingModel)
                    .Include(r => r.DataEtterKuttingOgSlipingModel)
                     .Include(r => r.ConcreteDensityModel)

                    .FirstOrDefaultAsync(r => r.Id == reportModel.Id);

                if (existingReport != null)
                {
                    existingReport.Tittle = reportModel.Tittle;
                    existingReport.Status = reportModel.Status;
                    existingReport.Kunde = reportModel.Kunde;


                    UpdateImages(context, existingReport, reportModel);
                    UpdatePrøver(context, existingReport, reportModel);
                    UpdateEtterKuttingPrøver(context, existingReport, reportModel);
                    UpdateConcreteDensity(context, existingReport, reportModel);
                    //Updatetrykketessting(context, existingReport, reportModel);

                    // Save changes to the context
                    await context.SaveChangesAsync();
                }
            }
        }
        private void UpdateImages(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            foreach (var existingimages in existingReport.Images.ToList())
            {
                if (!newReport.Images.Any(p => p.Id == existingimages.Id))
                {
                    context.Remove(existingimages);
                }
            }

            if (newReport.Images.Any())
            {
                foreach (var newImage in newReport.Images)
                {
                    if (!existingReport.Images.Any(p => p.Id == newImage.Id))
                    {
                        context.Add(newImage);
                    }
                }
            }
            else
            {
                context.RemoveRange(existingReport.Images);
            }

            context.SaveChanges();
        }


        private void UpdatePrøver(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            // Implementation here needs to handle TrykktestingModel as nested entities
            foreach (var existingPrøve in existingReport.DataFraOppdragsgiverPrøver.ToList())
            {
                var newPrøve = newReport.DataFraOppdragsgiverPrøver.FirstOrDefault(p => p.Id == existingPrøve.Id);
                if (newPrøve != null)
                {
                    // Update TrykktestingModel within each prøve
                    UpdateTrykktestingModels(context, existingPrøve, newPrøve);
                }
                else
                {
                    context.Remove(existingPrøve);
                }
            }

            foreach (var newPrøve in newReport.DataFraOppdragsgiverPrøver)
            {
                if (!existingReport.DataFraOppdragsgiverPrøver.Any(p => p.Id == newPrøve.Id))
                {
                    context.Add(newPrøve);
                }
            }

            context.SaveChanges();
        }

        private void UpdateTrykktestingModels(DbContext context, DataFraOppdragsgiverPrøverModel existingPrøve, DataFraOppdragsgiverPrøverModel newPrøve)
        {
            foreach (var existingTrykktesting in existingPrøve.TrykktestingModel.ToList())
            {
                if (!newPrøve.TrykktestingModel.Any(t => t.Id == existingTrykktesting.Id))
                {
                    context.Remove(existingTrykktesting);
                }
            }

            foreach (var newTrykktesting in newPrøve.TrykktestingModel)
            {
                if (!existingPrøve.TrykktestingModel.Any(t => t.Id == newTrykktesting.Id))
                {
                    context.Add(newTrykktesting);
                }
            }
        }

        private void UpdateEtterKuttingPrøver(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            foreach (var existingPrøve in existingReport.DataEtterKuttingOgSlipingModel.ToList())
            {
                if (!newReport.DataEtterKuttingOgSlipingModel.Any(p => p.Id == existingPrøve.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            if (newReport.DataEtterKuttingOgSlipingModel.Any())
            {
                foreach (var newPrøve in newReport.DataEtterKuttingOgSlipingModel)
                {
                    if (!existingReport.DataEtterKuttingOgSlipingModel.Any(p => p.Id == newPrøve.Id))
                    {
                        context.Add(newPrøve);
                    }
                }
            }
            else
            {
                context.RemoveRange(existingReport.DataEtterKuttingOgSlipingModel);
            }

            context.SaveChanges();
        }


        private void UpdateConcreteDensity(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            foreach (var existingPrøve in existingReport.ConcreteDensityModel.ToList())
            {
                if (!newReport.ConcreteDensityModel.Any(p => p.Id == existingPrøve.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            if (newReport.ConcreteDensityModel.Any())
            {
                foreach (var newPrøve in newReport.ConcreteDensityModel)
                {
                    if (!existingReport.ConcreteDensityModel.Any(p => p.Id == newPrøve.Id))
                    {
                        context.Add(newPrøve);
                    }
                }
            }
            else
            {
                context.RemoveRange(existingReport.ConcreteDensityModel);
            }

            context.SaveChanges();
        }



        //private void Updatetrykketessting(DbContext context, ReportModel existingReport, ReportModel newReport)
        //{
        //    // Remove existing prøver that are not present in the newReport
        //    foreach (var existingPrøve in existingReport.TrykktestingModel.ToList())
        //    {
        //        if (!newReport.TrykktestingModel.Any(p => p.Id == existingPrøve.Id))
        //        {
        //            context.Remove(existingPrøve);
        //        }
        //    }

        //    // Check if the newReport has any prøver, and remove all existing if no new prøver are provided
        //    if (newReport.TrykktestingModel.Any())
        //    {
        //        // Add new prøver from newReport, only if there are any
        //        foreach (var newPrøve in newReport.TrykktestingModel)
        //        {
        //            if (!existingReport.TrykktestingModel.Any(p => p.Id == newPrøve.Id))
        //            {
        //                context.Add(newPrøve);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // If newReport has no prøver, remove all existing ones
        //        context.RemoveRange(existingReport.TrykktestingModel);
        //    }

        //    // Save changes to the database after updates
        //    context.SaveChanges();
        //}

    }
}
