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
