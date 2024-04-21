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
