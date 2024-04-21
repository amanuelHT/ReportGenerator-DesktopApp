using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using Report_Generator_Domain.Models;
using System.Windows.Input;

namespace Final_project.ViewModels.TablesVM
{
    public partial class DataFraOppdragsgiverPrøverVM : ObservableObject
    {
        private readonly ReportStore _reportStore;
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

        // Constructor for new entries
        public DataFraOppdragsgiverPrøverVM(
            ReportStore reportStore,
            ModalNavigation modalNavigation,
            Guid reportid,
            DataFraOppdragsgiverTableVM dataFraOppdragsgiverTableVM)
        {
            _reportStore = reportStore;
            _modalNavigation = modalNavigation;
            _reportModelId = reportid;
            SubmitCommand = new SubmitDataFraOppgavegiverCommand(this, reportStore, _reportModelId, dataFraOppdragsgiverTableVM, modalNavigation);
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

        // Commands to handle UI actions
        public ICommand SubmitCommand { get; }


        [RelayCommand]
        public void Cancel()
        {
            _modalNavigation.Close();
        }

    }
}
