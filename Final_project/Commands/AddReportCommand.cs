using Domain.Models;
using Final_project.Other;
using Final_project.Stores;
using Final_project.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Final_project.Commands
{
    public class AddReportCommand : AsyncCommandBase
    {
        private readonly AddReportVM _addReportVM;
        private readonly ReportStore _reportStore;
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigation _modalWindow;

        public AddReportCommand(ModalNavigation modalWindow, AddReportVM addReportVM, ReportStore reportStore, NavigationStore navigationStore)
        {
            _modalWindow = modalWindow;
            _addReportVM = addReportVM;
            _reportStore = reportStore;
            _navigationStore = navigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                ReportModel reportModel = ReportModelFactory.CreateReportModel(_addReportVM.ReportFormVM);

                bool isValid = ReportValidator.IsValidReportModel(reportModel, _addReportVM.ReportFormVM);
                if (!isValid)
                {

                    return;
                }

                await _reportStore.Add(reportModel);

                MessageBox.Show("Report has been added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _modalWindow.Close();
            }
            catch (ValidationException ex)
            {

            }

        }
    }
}
