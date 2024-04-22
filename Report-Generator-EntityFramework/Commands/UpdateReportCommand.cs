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
                    .FirstOrDefaultAsync(r => r.Id == reportModel.Id);

                if (existingReport != null)
                {
                    existingReport.Tittle = reportModel.Tittle;
                    existingReport.Status = reportModel.Status;
                    existingReport.Kunde = reportModel.Kunde;

                    UpdatePrøver(context, existingReport, reportModel);
                    UpdateEtterKuttingPrøver(context, existingReport, reportModel);
                    UpdateCocretDensity(context, existingReport, reportModel);
                    Updatetrykketessting(context, existingReport, reportModel);
                    // Save changes to the context
                    await context.SaveChangesAsync();
                }
            }
        }

        private void UpdatePrøver(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            // Remove prøver that are no longer present
            foreach (var existingPrøve in existingReport.DataFraOppdragsgiverPrøver.ToList())
            {
                if (!newReport.DataFraOppdragsgiverPrøver.Any(p => p.Id == existingPrøve.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            // Add new prøver
            foreach (var newPrøve in newReport.DataFraOppdragsgiverPrøver)
            {
                if (!existingReport.DataFraOppdragsgiverPrøver.Any(p => p.Id == newPrøve.Id))
                {
                    context.Add(newPrøve);
                }
            }

            // Optionally, update existing prøver if they contain updatable properties
            foreach (var existingPrøve in existingReport.DataFraOppdragsgiverPrøver)
            {
                var newPrøve = newReport.DataFraOppdragsgiverPrøver.FirstOrDefault(p => p.Id == existingPrøve.Id);
                if (newPrøve != null)
                {
                    // Update properties based on the new model structure
                    existingPrøve.Datomottatt = newPrøve.Datomottatt;
                    existingPrøve.Overdekningoppgitt = newPrøve.Overdekningoppgitt;
                    existingPrøve.Dmax = newPrøve.Dmax;
                    existingPrøve.KjerneImax = newPrøve.KjerneImax;
                    existingPrøve.KjerneImin = newPrøve.KjerneImin;
                    existingPrøve.OverflateOK = newPrøve.OverflateOK;
                    existingPrøve.OverflateUK = newPrøve.OverflateUK;
                    // Additional fields can be updated here as necessary
                }
            }



        }

        private void UpdateEtterKuttingPrøver(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            // Remove prøver that are no longer present
            foreach (var existingPrøve in existingReport.DataEtterKuttingOgSlipingModel.ToList())
            {
                if (!newReport.DataEtterKuttingOgSlipingModel.Any(p => p.Id == existingPrøve.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            // Add new prøver
            foreach (var newPrøve in newReport.DataEtterKuttingOgSlipingModel)
            {
                if (!existingReport.DataEtterKuttingOgSlipingModel.Any(p => p.Id == newPrøve.Id))
                {
                    context.Add(newPrøve);
                }
            }

            // Optionally, update existing prøver if they contain updatable properties
            foreach (var existingPrøve in existingReport.DataEtterKuttingOgSlipingModel)
            {
                var newPrøve = newReport.DataEtterKuttingOgSlipingModel.FirstOrDefault(p => p.Id == existingPrøve.Id);
                if (newPrøve != null)
                {
                    // Update additional properties from reportModel
                    // Update additional properties from newPrøve (changed from reportModel)

                    existingPrøve.IvannbadDato = newPrøve.IvannbadDato;
                    existingPrøve.TestDato = newPrøve.TestDato;
                    existingPrøve.Overflatetilstand = newPrøve.Overflatetilstand;
                    existingPrøve.Dm = newPrøve.Dm;
                    existingPrøve.Prøvetykke = newPrøve.Prøvetykke;
                    existingPrøve.DmPrøvetykkeRatio = newPrøve.DmPrøvetykkeRatio;
                    existingPrøve.TrykkfasthetMPa = newPrøve.TrykkfasthetMPa;
                    existingPrøve.FasthetSammenligning = newPrøve.FasthetSammenligning;
                    existingPrøve.FørSliping = newPrøve.FørSliping;
                    existingPrøve.EtterSliping = newPrøve.EtterSliping;
                    existingPrøve.MmTilTopp = newPrøve.MmTilTopp;
                }
            }
        }

        private void UpdateCocretDensity(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            // Remove prøver that are no longer present
            foreach (var existingPrøve in existingReport.ConcreteDensityModel.ToList())
            {
                if (!newReport.ConcreteDensityModel.Any(p => p.Id == existingPrøve.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            // Add new prøver
            foreach (var newPrøve in newReport.ConcreteDensityModel)
            {
                if (!existingReport.ConcreteDensityModel.Any(p => p.Id == newPrøve.Id))
                {
                    context.Add(newPrøve);
                }
            }

            // Optionally, update existing prøver if they contain updatable properties
            foreach (var existingPrøve in existingReport.ConcreteDensityModel)
            {
                var newPrøve = newReport.ConcreteDensityModel.FirstOrDefault(p => p.Id == existingPrøve.Id);
                if (newPrøve != null)
                {
                    // Update only the updatable properties from newPrøve
                    existingPrøve.Dato = newPrøve.Dato;
                    existingPrøve.MasseILuft = newPrøve.MasseILuft;
                    existingPrøve.MasseIVannbad = newPrøve.MasseIVannbad; // Assuming this is also updatable
                    existingPrøve.Pw = newPrøve.Pw; // Assuming this is also updatable
                    existingPrøve.V = newPrøve.V; // Assuming this is also updatable
                    existingPrøve.Densitet = newPrøve.Densitet;
                }
            }

        }

        private void Updatetrykketessting(DbContext context, ReportModel existingReport, ReportModel newReport)
        {
            // Remove prøver that are no longer present
            foreach (var existingPrøve in existingReport.TrykktestingModel.ToList())
            {
                if (!newReport.TrykktestingModel.Any(p => p.Id == existingPrøve.Id))
                {
                    context.Remove(existingPrøve);
                }
            }

            // Add new prøver
            foreach (var newPrøve in newReport.TrykktestingModel)
            {
                if (!existingReport.TrykktestingModel.Any(p => p.Id == newPrøve.Id))
                {
                    context.Add(newPrøve);
                }
            }

            // Optionally, update existing prøver if they contain updatable properties
            foreach (var existingTrykktesting in existingReport.TrykktestingModel)
            {
                var newTrykktesting = newReport.TrykktestingModel.FirstOrDefault(p => p.Id == existingTrykktesting.Id);
                if (existingTrykktesting != null)
                {
                    existingTrykktesting.TrykkflateMm = newTrykktesting.TrykkflateMm;
                    existingTrykktesting.PalastHastighetMPas = newTrykktesting.PalastHastighetMPas;
                    existingTrykktesting.TrykkfasthetMPa = newTrykktesting.TrykkfasthetMPa;
                    existingTrykktesting.TrykkfasthetMPaNSE = newTrykktesting.TrykkfasthetMPaNSE;
                }
            }

        }


    }
}
