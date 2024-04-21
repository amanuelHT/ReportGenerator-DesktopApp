using Final_project.Stores;
using Final_project.ViewModels.TablesVM;
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

        public SubmitDataFraOppgavegiverCommand(DataFraOppdragsgiverPrøverVM viewModel, ReportStore reportStore, Guid reportmodelid, DataFraOppdragsgiverTableVM dataFraOppdragsgiverTableVM)
        {
            _viewModel = viewModel;
            _reportStore = reportStore;
            _reportmodelid = reportmodelid;
            _dataFraOppdragsgiverTableVM = dataFraOppdragsgiverTableVM;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Name) && !string.IsNullOrWhiteSpace(_viewModel.Description);
        }

        public void Execute(object? parameter)
        {
            //var newEntry = new DataFraOppdragsgiverPrøverModel(Guid.NewGuid(), _viewModel.Name, _viewModel.Description, _reportmodelid);

            // Create a new instance of DataFraOppdragsgiverPrøverVM for the new entry
            var newPrøveVM = new DataFraOppdragsgiverPrøverVM(_reportStore, modalNavigation, _reportmodelid, _dataFraOppdragsgiverTableVM)
            {
                Name = _viewModel.Name,
                Description = _viewModel.Description
            };

            // Add the new Prøve VM to the collection in DataFraOppdragsgiverTableVM
            _dataFraOppdragsgiverTableVM.Prøver.Add(newPrøveVM);

            // Optionally, you could clear the fields in the original viewModel if needed
            _viewModel.Name = "";
            _viewModel.Description = "";

            // Optionally, you can close the modal or navigate away if that's part of the process
            // _modalNavigation.CloseCurrent();



            //_reportStore.AddDataFraOppdragsgiverPrøver(newEntry);
            //_modalNavigation.CloseCurrent();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
