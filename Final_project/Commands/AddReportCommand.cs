using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{
    public class AddReportCommand : AsyncCommandBase
    {
        private readonly AddReportVM _addReportVM;
        private readonly ReportStore _reportStore;
        private readonly ModalNavigation _navigationStore;

        public AddReportCommand(AddReportVM addReportVM, ReportStore reportStore, ModalNavigation navigationStore)
        {
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

            try
            {
                await _reportStore.Add(reportModel);
                _navigationStore.Close();
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                throw;
            }
        }
    }
}
