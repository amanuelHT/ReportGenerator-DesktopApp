using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;
using Final_project.Views;
using Report_Generator_Domain.Models;

namespace Final_project.Commands
{
    public class AddReportCommand : AsyncCommandBase
    {
        private readonly AddReportVM _addReportVM;
        private readonly ReportStore _reportStore;
        private readonly NavigationStore _navigationStore;
        private readonly ModalWindow _modalWindow;


        public AddReportCommand(ModalWindow modalWindow, AddReportVM addReportVM, ReportStore reportStore, NavigationStore navigationStore)
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

            // Assuming the ImageCollectionVM is part of your ReportFormVM
            foreach (var Prøver in reportForm.DataFraOppdragsgiverTableVM.Prøver)
            {
                DataFraOppdragsgiverPrøverModel prøverModel = new DataFraOppdragsgiverPrøverModel(
                       Guid.NewGuid(),
                       Prøver.Name,
                       Prøver.Description,
                       reportModel.Id); // Use 'reportModel' instead of 'newReport'
                reportModel.DataFraOppdragsgiverPrøver.Add(prøverModel);
            }




            try
            {
                await _reportStore.Add(reportModel);

                //// Assuming modalWindow is the instance of ModalWindow
                //_modalWindow.Dispatcher.Invoke(() =>
                //{
                //    _modalWindow.DialogResult = true; // or false depending on the scenario
                //    _modalWindow.Close();
                //});



            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                throw;
            }
        }
    }
}
