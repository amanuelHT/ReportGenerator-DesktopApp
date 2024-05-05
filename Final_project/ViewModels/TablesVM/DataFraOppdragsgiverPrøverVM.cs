using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using Report_Generator_Domain.Models;

namespace Final_project.ViewModels.TablesVM
{
    public partial class DataFraOppdragsgiverPrøverVM : ObservableObject
    {

        private readonly ModalNavigation _modalNavigation;
        private Guid _reportModelId;

        // Properties based on the model fields
        [ObservableProperty]
        private DateTime datomottatt;

        [ObservableProperty]
        private string overdekningoppgitt;

        [ObservableProperty]
        private string dmax;

        [ObservableProperty]
        private int kjerneImax;

        [ObservableProperty]
        private int kjerneImin;

        [ObservableProperty]
        private string overflateOK;

        [ObservableProperty]
        private string overflateUK;

        public DataFraOppdragsgiverTableVM _dataFraOppdragsgiverTableVM { get; }

        // Constructor for new entries
        public DataFraOppdragsgiverPrøverVM(

            ModalNavigation modalNavigation,
            Guid reportid,
            DataFraOppdragsgiverTableVM dataFraOppdragsgiverTableVM)
        {

            _modalNavigation = modalNavigation;
            _reportModelId = reportid;
            _dataFraOppdragsgiverTableVM = dataFraOppdragsgiverTableVM;
        }

        // Constructor to populate from an existing model
        public DataFraOppdragsgiverPrøverVM(DataFraOppdragsgiverPrøverModel model)
        {
            if (model != null)
            {
                Datomottatt = model.Datomottatt;
                Overdekningoppgitt = model.Overdekningoppgitt;
                Dmax = model.Dmax;
                KjerneImax = model.KjerneImax;
                KjerneImin = model.KjerneImin;
                OverflateOK = model.OverflateOK;
                OverflateUK = model.OverflateUK;
                _reportModelId = model.ReportModelId;
            }
        }




        [RelayCommand]
        public void Cancel()
        {
            _modalNavigation.Close();
        }



        [RelayCommand]
        public virtual void Submit()
        {
            var _viewModel = this;

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
                    _reportModelId
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
            _viewModel.Datomottatt = DateTime.Now; // Reset to current time or a default value
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
    }
}

