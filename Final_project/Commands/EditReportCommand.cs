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

        public EditReportCommand(ModalNavigation modalNavigation, EditReportVM editReportVM, ReportStore reportStore, NavigationStore navigationStore)
        {
            ModalNavigation = modalNavigation;
            _editReportVM = editReportVM;
            _reportStore = reportStore;
            _navigationStore = navigationStore;
        }

        public ModalNavigation ModalNavigation { get; }

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
                ModalNavigation.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
