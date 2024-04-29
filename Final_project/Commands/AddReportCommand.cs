using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;
using Report_Generator_Domain.Models;
using System.Windows;

namespace Final_project.Commands
{
    public class AddReportCommand : AsyncCommandBase
    {
        private readonly AddReportVM _addReportVM;
        private readonly ReportStore _reportStore;
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigation _modalWindow;


        public AddReportCommand(ModalNavigation modalWindow, AddReportVM addReportVM, ReportStore reportStore, NavigationStore navigationStore)
        {
            _modalWindow = modalWindow;
            _addReportVM = addReportVM;
            _reportStore = reportStore;
            _navigationStore = navigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ReportFormVM reportForm = _addReportVM.ReportFormVM;

            ReportModel reportModel = new ReportModel(
                Guid.NewGuid(),
                reportForm.Tittle, // Ensure 'Tittle' is the intended property name (commonly 'Title')
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

                    reportModel.TrykktestingModel.Add(trykktestingModel);  // Add the newly created model to the report model
                }
                else
                {
                    // Handle the case where trykktesting entry is null
                    // Possible actions: log an error, display a message, etc.
                    Console.WriteLine("Trykktesting entry is null.");
                }
            }

            bool success = false;
            try
            {
                await _reportStore.Add(reportModel);
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the report: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (success)
                {
                    MessageBox.Show("Report has been added successfully.");


                };



            }
        }
    }
}
