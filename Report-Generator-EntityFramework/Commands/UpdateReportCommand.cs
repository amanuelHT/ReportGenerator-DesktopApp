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
                   .Include(r => r.Test)
                   .Include(r => r.Verktøy)
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
                    UpdateTests(context, existingReport, reportModel);
                    UpdateVerktøy(context, existingReport, reportModel);
                    UpdatePrøver(context, existingReport, reportModel);

                    await context.SaveChangesAsync();
                }
            }
        }

        private void UpdateImages(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            foreach (var existingImage in existingReport.Images.ToList())
            {
                if (!newReport.Images.Any(p => p.Id == existingImage.Id))
                {
                    context.Remove(existingImage);
                }
            }

            foreach (var newImage in newReport.Images)
            {
                if (!existingReport.Images.Any(p => p.Id == newImage.Id))
                {
                    context.Add(newImage);
                }
            }
        }

        private void UpdateTests(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            foreach (var existingTest in existingReport.Test.ToList())
            {
                if (!newReport.Test.Any(p => p.Id == existingTest.Id))
                {
                    context.Remove(existingTest);
                }
            }

            foreach (var newTest in newReport.Test)
            {
                if (!existingReport.Test.Any(p => p.Id == newTest.Id))
                {
                    context.Add(newTest);
                }
            }
        }

        private void UpdateVerktøy(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            foreach (var existingVerktøy in existingReport.Verktøy.ToList())
            {
                if (!newReport.Verktøy.Any(p => p.Id == existingVerktøy.Id))
                {
                    context.Remove(existingVerktøy);
                }
            }

            foreach (var newVerktøy in newReport.Verktøy)
            {
                if (!existingReport.Verktøy.Any(p => p.Id == newVerktøy.Id))
                {
                    context.Add(newVerktøy);
                }
            }
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
                    UpdateEtterKuttingPrøver(context, existingPrøve, newPrøve);
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
                    context.Remove(existingConcreteDensity);
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
                    context.Remove(existingEtterKuttingPrøver);
                }
            }

            foreach (var newEtterKuttingPrøver in newPrøve.DataEtterKuttingOgSlipingModel)
            {
                if (!existingPrøve.DataEtterKuttingOgSlipingModel.Any(t => t.Id == newEtterKuttingPrøver.Id))
                {
                    context.Add(newEtterKuttingPrøver);
                }
            }
        }
    }
}






