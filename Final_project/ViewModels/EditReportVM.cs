using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using Final_project.ViewModels.ReportComponentsVM;
using Final_project.ViewModels.TablesVM;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class EditReportVM : ObservableObject
    {
        private readonly ReportStore _reportStore;

        public Guid ReportId { get; private set; }
        public ReportFormVM ReportFormVM { get; private set; }

        public EditReportVM(ReportModel reportModel, ReportStore reportStore, ModalNavigation modalNavigation, NavigationStore navigationStore, ModalNavigation modalWindowVM)
        {
            _reportStore = reportStore;
            if (reportModel == null)
            {
                throw new ArgumentNullException(nameof(reportModel));
            }

            ReportId = reportModel.Id;

            ICommand submitCommand = new EditReportCommand(modalWindowVM, this, reportStore, navigationStore);
            ICommand cancelCommand = new CloseModalCommand(navigationStore);

            // Pass ReportId to ReportFormVM
            ReportFormVM = new ReportFormVM(submitCommand, cancelCommand, reportStore, modalNavigation, ReportId)
            {
                Tittle = reportModel.Tittle,
                Status = reportModel.Status,
                Kunde = reportModel.Kunde,
                AvvikFraStandarder = reportModel.AvvikFraStandarder,
                MotattDato = reportModel.MotattDato,
                Kommentarer = reportModel.Kommentarer,
                UiaRegnr = reportModel.UiaRegnr,

            };


            if (reportModel.TestUtførtAvModel != null)
            {
                ReportFormVM.TestUtførtAvVM = new TestUtførtAvVM(reportModel.TestUtførtAvModel);
            }

            if (reportModel.KontrollertAvførtAvModel != null)
            {
                ReportFormVM.KontrollertAvVM = new KontrollertAvVM(reportModel.KontrollertAvførtAvModel);
            }


            foreach (var img in reportModel.Images)
            {
                var imageVM = new ImageVM(img);
                ReportFormVM.ImageCollectionViewModel.Images.Add(imageVM);
            }


            foreach (var test in reportModel.Test)
            {
                var testVM = new TestVM(test);
                ReportFormVM.TestCollectionVM.tests.Add(testVM);
            }

            foreach (var verktøy in reportModel.Verktøy)
            {
                var verktøyVM = new VerktøyVM(verktøy);
                ReportFormVM.VerktøyCollectionVM.verktøyVMs.Add(verktøyVM);
            }



            foreach (var prøve in reportModel.DataFraOppdragsgiverPrøver)
            {
                var prøveVM = new DataFraOppdragsgiverPrøverVM(prøve);
                ReportFormVM.DataFraOppdragsgiverTableVM.Prøver.Add(prøveVM);
            }


            foreach (var prøve in reportModel.DataEtterKuttingOgSlipingModel)
            {
                var prøveVM = new DataEtterKuttingOgSlipingPrøveVM(prøve);
                ReportFormVM.DataEtterKuttingOgSlipingTableVM.Prøver.Add(prøveVM);
            }

            foreach (var densityModel in reportModel.ConcreteDensityModel)
            {
                var densityPrøveVM = new ConcreteDensityPrøveVM(densityModel);
                ReportFormVM.ConcreteDensityTableVM.Prøver.Add(densityPrøveVM);
            }

            foreach (var trykktestingModel in reportModel.TrykktestingModel)
            {
                var trykktesting = new TrykktestingPrøveVM(trykktestingModel);
                ReportFormVM.TrykktestingTableVM.Trykketester.Add(trykktesting);
            }
        }
    }
}