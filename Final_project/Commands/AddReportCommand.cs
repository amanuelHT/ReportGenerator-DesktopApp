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
                reportForm.Tittle,
                reportForm.Status,
                reportForm.Kunde
            );

            List<ReportImageModel> images = new List<ReportImageModel>();
            foreach (var imageVM in reportForm.ImageCollectionViewModel.Images)
            {
                var imageUri = imageVM.ImageUri;
                var imageId = Guid.NewGuid(); // Generate a new Guid for each image
                var imageName = System.IO.Path.GetFileName(imageUri.LocalPath); // Extracts the file name

                images.Add(new ReportImageModel(imageId, imageName, imageUri.ToString()));
            }

            try
            {
                // Now passing both the reportModel and its images
                await _reportStore.Add(reportModel, images);

                _navigationStore.Close();
            }
            catch (Exception ex)
            {
                // Consider adding some error handling logic here
                throw;
            }
        }
    }
}
