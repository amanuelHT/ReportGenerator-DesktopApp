using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{
    internal class EditReportCommand : AsyncCommandBase
    {
        private readonly ReportStore _reportStore;
        private readonly EditReportVM _editReportVM;
        private readonly ModalNavigation _navigationStore;

        public EditReportCommand(EditReportVM editReportVM, ReportStore reportStore, ModalNavigation navigationStore)
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

            // Filter images to remove duplicates based on URL
            List<ReportImageModel> images = reportForm.ImageCollectionViewModel.Images
                .GroupBy(img => img.ImageUri.ToString())
                .Select(grp => grp.First()) // Take only the first occurrence of each unique image URL
                .Select(imageVM => new ReportImageModel(
                    Guid.NewGuid(), // Generate a new Guid for each image
                    System.IO.Path.GetFileName(imageVM.ImageUri.LocalPath), // Extracts the file name
                    imageVM.ImageUri.ToString()
                ))
                .ToList();

            try
            {
                await _reportStore.Update(reportModel, images);
                _navigationStore.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
