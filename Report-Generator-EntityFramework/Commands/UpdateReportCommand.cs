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
    }
}
