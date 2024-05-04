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
                   .Include(r => r.Images)
                   .Include(r => r.DataFraOppdragsgiverPrøver)
                        .ThenInclude(p => p.TrykktestingModel)
                    .Include(r => r.DataFraOppdragsgiverPrøver)
                        .ThenInclude(t => t.ConcreteDensityModel)
                   .Include(r => r.DataFraOppdragsgiverPrøver)
                        .ThenInclude(r => r.DataEtterKuttingOgSlipingModel)
                   .FirstOrDefaultAsync(r => r.Id == reportModel.Id);

                if (existingReport != null)
                {
                    existingReport.Tittle = reportModel.Tittle;
                    existingReport.Status = reportModel.Status;
                    existingReport.Kunde = reportModel.Kunde;


                    UpdateImages(context, existingReport, reportModel);
                    UpdatePrøver(context, existingReport, reportModel);



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
            foreach (var existingPrøve in existingReport.DataFraOppdragsgiverPrøver.ToList())
            {
                var newPrøve = newReport.DataFraOppdragsgiverPrøver.FirstOrDefault(p => p.Id == existingPrøve.Id);
                if (newPrøve != null)
                {

                    UpdateTrykktestingModels(context, existingPrøve, newPrøve);
                    UpdateConcreteDensity(context, existingPrøve, newPrøve);
                    UpdateConcreteDensity(context, existingPrøve, newPrøve);

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

        private void UpdateConcreteDensity(DbContext context, DataFraOppdragsgiverPrøverModel existingPrøve, DataFraOppdragsgiverPrøverModel newPrøve)
        {
            foreach (var existingConcreteDensity in existingPrøve.ConcreteDensityModel.ToList())
            {
                if (!newPrøve.ConcreteDensityModel.Any(p => p.Id == existingConcreteDensity.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            foreach (var newConcreteDensity in newPrøve.ConcreteDensityModel)
            {
                if (!existingPrøve.ConcreteDensityModel.Any(t => t.Id == newConcreteDensity.Id))
                {
                    context.Add(newConcreteDensity);
                }
            }
        }

        private void UpdateEtterKuttingPrøver(DbContext context, DataFraOppdragsgiverPrøverModel existingPrøve, DataFraOppdragsgiverPrøverModel newPrøve)
        {
            foreach (var existingEtterKuttingPrøver in existingPrøve.DataEtterKuttingOgSlipingModel.ToList())
            {
                if (!newPrøve.DataEtterKuttingOgSlipingModel.Any(p => p.Id == existingEtterKuttingPrøver.Id))
                {
                    context.Remove(existingPrøve);
                }
            }


            foreach (var newEtterKuttingPrøver in newPrøve.ConcreteDensityModel)
            {
                if (!existingPrøve.ConcreteDensityModel.Any(t => t.Id == newEtterKuttingPrøver.Id))
                {
                    context.Add(newEtterKuttingPrøver);
                }
            }
        }




    }
}
