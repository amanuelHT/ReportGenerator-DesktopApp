using Domain.Models;
using Final_project.ViewModels;
using Report_Generator_Domain.Models;

namespace Final_project.Other
{
    public class ReportModelFactory
    {
        public static ReportModel CreateReportModel(ReportFormVM reportForm, Guid? reportId = null)
        {
            if (reportForm == null)
            {
                throw new ArgumentNullException(nameof(reportForm), "ReportFormVM cannot be null.");
            }

            // If reportId is not provided, generate a new GUID
            Guid id = reportId ?? Guid.NewGuid();


            ReportModel reportModel = new ReportModel(
                id,
                reportForm.Tittle,
                reportForm.Status,
                reportForm.Kunde
            );


            if (reportForm.ImageCollectionViewModel != null)
            {
                foreach (var imageVM in reportForm.ImageCollectionViewModel.Images)
                {
                    if (imageVM != null)
                    {
                        ReportImageModel imageModel = new ReportImageModel(
                            imageVM.ImageId,
                            imageVM.ImageName,
                            imageVM.ImageUri?.ToString(),
                            reportModel.Id
                        );
                        reportModel.Images.Add(imageModel);
                    }
                }
            }
            if (reportForm.VerktøyCollectionVM != null)
            {
                foreach (var verktøy1 in reportForm.VerktøyCollectionVM.verktøyVMs)
                {
                    if (verktøy1 != null)
                    {
                        verktøyModel verktøyModel = new verktøyModel(
                           Guid.NewGuid(),
                            verktøy1.Name,
                            reportModel.Id

                            );
                        reportModel.Verktøy.Add(verktøyModel);


                    }
                }
            }


            if (reportForm.TestCollectionVM != null)
            {
                foreach (var test in reportForm.TestCollectionVM.tests)
                {
                    if (test != null)
                    {
                        TestModel testModel = new TestModel(
                               Guid.NewGuid(),
                               test.Name,
                               reportModel.Id
                        );
                        reportModel.Test.Add(testModel);
                    }
                }
            }


            if (reportForm.DataFraOppdragsgiverTableVM != null)
            {
                foreach (var prøveVM in reportForm.DataFraOppdragsgiverTableVM.Prøver)
                {
                    if (prøveVM != null)
                    {
                        DataFraOppdragsgiverPrøverModel prøverModel = new DataFraOppdragsgiverPrøverModel(
                            Guid.NewGuid(),
                            prøveVM.Datomottatt,
                            prøveVM.Overdekningoppgitt,
                            prøveVM.Dmax,
                            prøveVM.KjerneImax,
                            prøveVM.KjerneImin,
                            prøveVM.OverflateOK,
                            prøveVM.OverflateUK,
                            reportModel.Id
                        );

                        foreach (var trykktesting in reportForm.TrykktestingTableVM.Trykketester)
                        {
                            if (trykktesting != null)
                            {
                                TrykktestingModel trykktestingModel = new TrykktestingModel(
                                    Guid.NewGuid(),
                                    trykktesting.TrykkflateMm,
                                    trykktesting.PalastHastighetMPas,
                                    trykktesting.TrykkfasthetMPa,
                                    trykktesting.TrykkfasthetMPaNSE,
                                    reportModel.Id
                                );
                                prøverModel.TrykktestingModel.Add(trykktestingModel);
                            }
                        }



                        foreach (var prøve in reportForm.ConcreteDensityTableVM.Prøver)
                        {
                            if (prøve != null)
                            {
                                ConcreteDensityModel concreteDensityModel = new ConcreteDensityModel(
                                    prøve.Provnr,
                                    prøve.Dato,
                                    prøve.MasseILuft,
                                    prøve.MasseIVannbad,
                                    prøve.Pw,
                                    prøve.V,
                                    prøve.Densitet,
                                    reportModel.Id
                                );
                                prøverModel.ConcreteDensityModel.Add(concreteDensityModel);
                            }
                        }

                        foreach (var prøve in reportForm.DataEtterKuttingOgSlipingTableVM.Prøver)
                        {
                            if (prøve != null)
                            {
                                DataEtterKuttingOgSlipingModel dataEtterKuttingOgSlipingModel = new DataEtterKuttingOgSlipingModel(
                                    Guid.NewGuid(),
                                    prøve.IvannbadDato,
                                    prøve.TestDato,
                                    prøve.Overflatetilstand,
                                    prøve.Dm,
                                    prøve.Prøvetykke,
                                    prøve.DmPrøvetykkeRatio,
                                    prøve.TrykkfasthetMPa,
                                    prøve.FasthetSammenligning,
                                    prøve.FørSliping,
                                    prøve.EtterSliping,
                                    prøve.MmTilTopp,
                                    reportModel.Id
                                );
                                prøverModel.DataEtterKuttingOgSlipingModel.Add(dataEtterKuttingOgSlipingModel);
                            }
                        }

                        reportModel.DataFraOppdragsgiverPrøver.Add(prøverModel);
                    }
                }
            }







            return reportModel;
        }
    }
}
