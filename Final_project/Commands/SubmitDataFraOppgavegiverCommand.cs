using Final_project.Stores;
using Final_project.ViewModels.TablesVM;
using Report_Generator_Domain.Models;
using System.Windows.Input;

namespace Final_project.Commands
{
    public class SubmitDataFraOppgavegiverCommand : ICommand
    {
        private readonly DataFraOppdragsgiverPrøverVM _viewModel;
        private readonly ReportStore _reportStore;
        private readonly Guid _reportmodelid;
        private readonly DataFraOppdragsgiverTableVM _dataFraOppdragsgiverTableVM;
        private readonly ModalNavigation _modalNavigation;
        private readonly ModalNavigation modalNavigation;

        public SubmitDataFraOppgavegiverCommand(DataFraOppdragsgiverPrøverVM viewModel, ReportStore reportStore, Guid reportmodelid, DataFraOppdragsgiverTableVM dataFraOppdragsgiverTableVM, ModalNavigation modalNavigation)
        {
            _viewModel = viewModel;
            _reportStore = reportStore;
            _reportmodelid = reportmodelid;
            _dataFraOppdragsgiverTableVM = dataFraOppdragsgiverTableVM;
            _modalNavigation = modalNavigation;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            // Create a new entry model using the current state of the ViewModel
            var newEntry = new DataFraOppdragsgiverPrøverModel(
                Guid.NewGuid(),
                _viewModel.Datomottatt,
                _viewModel.Overdekningoppgitt,
                _viewModel.Dmax,
                _viewModel.KjerneImax,
                _viewModel.KjerneImin,
                _viewModel.OverflateOK,
                _viewModel.OverflateUK,
                _reportmodelid
            );

            // Create a new instance of DataFraOppdragsgiverPrøverVM for the new entry
            var newPrøveVM = new DataFraOppdragsgiverPrøverVM(newEntry)
            {
                // If other properties need to be set directly, set them here
                // Additional properties set up could be placed here if needed
            };

            // Add the new Prøve VM to the collection in DataFraOppdragsgiverTableVM
            _dataFraOppdragsgiverTableVM.Prøver.Add(newPrøveVM);

            // Optionally, clear or reset the ViewModel's properties if needed for a new entry
            _viewModel.Datomottatt = DateTime.MaxValue; // Reset to current time or a default value
            _viewModel.Overdekningoppgitt = "";
            _viewModel.Dmax = "";
            _viewModel.KjerneImax = 0;
            _viewModel.KjerneImin = 0;
            _viewModel.OverflateOK = "";
            _viewModel.OverflateUK = "";

            _modalNavigation.Close();
            // Optionally, handle modal navigation or UI feedback
            // _modalNavigation.CloseCurrent(); // Close the modal if that's part of your application flow
            // You may want to handle additional UI updates or confirmations here
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
