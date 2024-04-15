using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;

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

            try
            {
                await _reportStore.Update(reportModel);
                reportForm.CancelCommand.Execute(null);


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
