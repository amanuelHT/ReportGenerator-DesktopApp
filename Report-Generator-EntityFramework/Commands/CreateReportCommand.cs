using Domain.Models;
using Report_Generator_Domain.Commands;

namespace Report_Generator_EntityFramework.Commands
{
    public class CreateReportCommand : ICreateReportCommand
    {
        private readonly ReportModelDbContextFactory _contextFactory;

        public CreateReportCommand(ReportModelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(ReportModel reportModel)
        {
            using (ReportModelDbContext context = _contextFactory.Create())
            {
                // Add the report model to the context
                context.ReportModels.Add(reportModel);


                // Add associated images to the context
                foreach (var images in reportModel.Images)
                {
                    context.ReportImageModels.Add(images);
                }


                foreach (var tests in reportModel.Test)
                {
                    context.tests.Add(tests);
                }


                foreach (var verktøy in reportModel.Verktøy)
                {
                    context.verktøies.Add(verktøy);
                }



                foreach (var prøver in reportModel.DataFraOppdragsgiverPrøver)
                {
                    context.DataFraOppdragsgiverPrøverModels.Add(prøver);

                    // Here we should add the TrykktestingModel related to each DataFraOppdragsgiverPrøverModel
                    foreach (var trykktesting in prøver.TrykktestingModel)
                    {
                        context.trykktestingModels.Add(trykktesting);
                    }

                    foreach (var density in prøver.ConcreteDensityModel)
                    {
                        context.concreteDensityModels.Add(density);
                    }


                    foreach (var dataEtterKuttingOgSlipingModel in prøver.DataEtterKuttingOgSlipingModel)
                    {
                        context.DataEtterKuttingOgSlipingModels.Add(dataEtterKuttingOgSlipingModel);
                    }

                }






                // Save changes to the database
                await context.SaveChangesAsync();
            }
        }
    }
}
