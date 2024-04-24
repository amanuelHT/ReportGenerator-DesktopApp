using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Report_Generator_Domain.Commands;

namespace Report_Generator_EntityFramework.Commands
{
    public class UpdateReportCommand : IUpdateReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;
        private readonly ICreateImageCommand _createImageCommand;

        public UpdateReportCommand(ReportModelDbContextFactory contextFactory, ICreateImageCommand createImageCommand)
        {
            _contextFactory = contextFactory;
            _createImageCommand = createImageCommand;
        }

        public async Task Execute(ReportModel reportModel)
        {
            using (var context = _contextFactory.Create())
            {
                var existingReport = await context.ReportModels
                    .Include(r => r.Images) // Include images for updating
                    .Include(r => r.DataFraOppdragsgiverPrøver) // Include prøver for updating
                    .Include(r => r.DataEtterKuttingOgSlipingModel)
                     .Include(r => r.ConcreteDensityModel)
                     .Include(r => r.TrykktestingModel)
                    .FirstOrDefaultAsync(r => r.Id == reportModel.Id);

                if (existingReport != null)
                {
                    existingReport.Tittle = reportModel.Tittle;
                    existingReport.Status = reportModel.Status;
                    existingReport.Kunde = reportModel.Kunde;

                    UpdatePrøver(context, existingReport, reportModel);
                    UpdateEtterKuttingPrøver(context, existingReport, reportModel);
                    UpdateConcreteDensity(context, existingReport, reportModel);
                    Updatetrykketessting(context, existingReport, reportModel);
                    // Save changes to the context
                    await context.SaveChangesAsync();
                }
            }
        }

        private void UpdatePrøver(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            foreach (var existingPrøve in existingReport.DataFraOppdragsgiverPrøver.ToList())
            {
                if (!newReport.DataFraOppdragsgiverPrøver.Any(p => p.Id == existingPrøve.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            if (newReport.DataFraOppdragsgiverPrøver.Any())
            {
                foreach (var newPrøve in newReport.DataFraOppdragsgiverPrøver)
                {
                    if (!existingReport.DataFraOppdragsgiverPrøver.Any(p => p.Id == newPrøve.Id))
                    {
                        context.Add(newPrøve);
                    }
                }
            }
            else
            {
                context.RemoveRange(existingReport.DataFraOppdragsgiverPrøver);
            }

            context.SaveChanges();
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



        private void Updatetrykketessting(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            // Remove existing prøver that are not present in the newReport
            foreach (var existingPrøve in existingReport.TrykktestingModel.ToList())
            {
                if (!newReport.TrykktestingModel.Any(p => p.Id == existingPrøve.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            // Check if the newReport has any prøver, and remove all existing if no new prøver are provided
            if (newReport.TrykktestingModel.Any())
            {
                // Add new prøver from newReport, only if there are any
                foreach (var newPrøve in newReport.TrykktestingModel)
                {
                    if (!existingReport.TrykktestingModel.Any(p => p.Id == newPrøve.Id))
                    {
                        context.Add(newPrøve);
                    }
                }
            }
            else
            {
                // If newReport has no prøver, remove all existing ones
                context.RemoveRange(existingReport.TrykktestingModel);
            }

            // Save changes to the database after updates
            context.SaveChanges();
        }

    }
}
