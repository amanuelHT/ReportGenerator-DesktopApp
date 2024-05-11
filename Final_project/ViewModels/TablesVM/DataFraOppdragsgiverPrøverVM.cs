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
        public Guid? _dataId;


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
            Guid newId = _dataId ?? Guid.NewGuid();

            // Create a new entry model using the current state of the ViewModel
            var newEntry = new DataFraOppdragsgiverPrøverModel(
                     newId,
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

            };

            _dataFraOppdragsgiverTableVM.Prøver.Add(newPrøveVM);
            _modalNavigation.Close();

        }
    }
}

