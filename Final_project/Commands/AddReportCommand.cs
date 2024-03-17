using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{

    public class AddReportCommand : AsyncCommandBase
    {
        private readonly AddReportVM _addReportVM;
        private readonly ReportStore _reportStore;
        private readonly NavigationStore _navigationStore;
        public AddReportCommand(AddReportVM addReportVM, ReportStore reportStore, NavigationStore navigationStore)
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
                reportForm.Kunde);


            try
            {
                await _reportStore.Add(reportModel);

                _navigationStore.Close();

            }
            catch (Exception)
            {
                throw;

            }






        }
    }

}