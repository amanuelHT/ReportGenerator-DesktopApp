using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;
using Report_Generator_Domain.Models;

namespace Final_project.Commands
{
    internal class EditReportCommand : AsyncCommandBase
    {
        private readonly ReportStore _reportStore;
        private readonly EditReportVM _editReportVM;
        private readonly NavigationStore _navigationStore;

        public EditReportCommand(EditReportVM editReportVM, ReportStore reportStore, NavigationStore navigationStore)
        {
            _editReportVM = editReportVM;
            _reportStore = reportStore;
            _navigationStore = navigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ReportFormVM reportForm = _editReportVM.ReportFormVM;

            ReportModel reportModel = new ReportModel(
                _editReportVM.ReportId,
                reportForm.Tittle,
                reportForm.Status,
                reportForm.Kunde



            );

            // Assuming the ImageCollectionVM is part of your ReportFormVM
            foreach (var imageVM in reportForm.ImageCollectionViewModel.Images)
            {
                ReportImageModel imageModel = new ReportImageModel(
                    imageVM.ImageId,
                    imageVM.ImageName,
                    imageVM.ImageUri.ToString(),
                    reportModel.Id); // Use 'reportModel' instead of 'newReport'
                reportModel.Images.Add(imageModel);
            }


            foreach (var Prøver in reportForm.DataFraOppdragsgiverTableVM.Prøver)
            {
                DataFraOppdragsgiverPrøverModel prøverModel = new DataFraOppdragsgiverPrøverModel(
                       Guid.NewGuid(),
                       Prøver.Datomottatt,
                       Prøver.Overdekningoppgitt,
                       Prøver.Dmax,
                       Prøver.KjerneImax,
                       Prøver.KjerneImin,
                       Prøver.OverflateOK,
                       Prøver.OverflateUK,
                       reportModel.Id); // Use 'reportModel' instead of 'newReport'
                reportModel.DataFraOppdragsgiverPrøver.Add(prøverModel);
            }

            foreach (var Prøver in reportForm.DataEtterKuttingOgSlipingTableVM.Prøver)
            {
                // Check if Prøver is null before creating a new DataEtterKuttingOgSlipingModel
                if (Prøver != null)
                {
                    DataEtterKuttingOgSlipingModel prøverModel = new DataEtterKuttingOgSlipingModel(
                        Guid.NewGuid(),
                        Prøver.IvannbadDato,
                        Prøver.TestDato,
                        Prøver.Overflatetilstand,
                        Prøver.Dm,
                        Prøver.Prøvetykke,
                        Prøver.DmPrøvetykkeRatio,
                        Prøver.TrykkfasthetMPa,
                        Prøver.FasthetSammenligning,
                        Prøver.FørSliping,
                        Prøver.EtterSliping,
                        Prøver.MmTilTopp,
                        reportModel.Id
                    );

                    reportModel.DataEtterKuttingOgSlipingModel.Add(prøverModel);
                }
                else
                {
                    // Handle the case where Prøver is null
                    // (e.g., log an error, display a message to the user)
                    Console.WriteLine("Prøver object is null.");
                }
            }

            foreach (var prøve in reportForm.ConcreteDensityTableVM.Prøver)
            {
                // Check if prøve is null before creating a new DataPrøverModel
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

                    reportModel.ConcreteDensityModel.Add(concreteDensityModel);  // Add the newly created model to the report model
                }
                else
                {
                    // Handle the case where prøve is null
                    // (e.g., log an error, display a message to the user)
                    Console.WriteLine("Prøve object is null.");
                }
            }


            try
            {
                await _reportStore.Update(reportModel);



            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
