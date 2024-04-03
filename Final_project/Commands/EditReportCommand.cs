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
<<<<<<< HEAD
                reportForm.Tittle,
                reportForm.Status,
                reportForm.Kunde,
                reportForm.Images
            ) ;
=======
                  reportForm.Tittle,
                  reportForm.Status,
                  reportForm.Kunde,
                  reportForm.Images
            );


>>>>>>> 97a6e3179d9a800a68fc242a1e71c9c37fb6aee6


            try
            {
                await _reportStore.Update(reportModel);


                _navigationStore.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
