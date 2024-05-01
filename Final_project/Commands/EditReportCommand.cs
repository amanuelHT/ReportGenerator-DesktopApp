using Domain.Models;
using Final_project.Other;
using Final_project.Stores;
using Final_project.ViewModels;
using System.Windows;

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
            try
            {
                ReportModel reportModel = ReportModelFactory.CreateReportModel(_editReportVM.ReportFormVM, _editReportVM.ReportId);

                bool isValid = ReportValidator.IsValidReportModel(reportModel, _editReportVM.ReportFormVM);
                if (!isValid)
                {
                    return;
                }

                await _reportStore.Update(reportModel);
                MessageBox.Show("Report has been successfully Updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
