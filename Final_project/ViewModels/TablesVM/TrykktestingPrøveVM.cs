using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using Report_Generator_Domain.Models;

namespace Final_project.ViewModels.TablesVM
{
    public partial class TrykktestingPrøveVM : ObservableObject
    {

        private readonly ModalNavigation _modalNavigation;
        private Guid _reportModelId;
        public Guid TrykktestingId;

        private readonly TrykktestingTableVM _trykktestingTableVM;



        [ObservableProperty]
        private decimal trykkflateMm;

        [ObservableProperty]
        private decimal palastHastighetMPas;

        [ObservableProperty]
        private decimal trykkfasthetMPa;

        [ObservableProperty]
        private decimal trykkfasthetMPaNSE;

        // Constructor for new entries
        public TrykktestingPrøveVM(
            ModalNavigation modalNavigation,
            Guid reportid,
            TrykktestingTableVM trykktestingTableVM)
        {

            _modalNavigation = modalNavigation;
            _reportModelId = reportid;
            _trykktestingTableVM = trykktestingTableVM;
        }

        // Constructor to populate from an existing model
        public TrykktestingPrøveVM(TrykktestingModel model)
        {
            if (model != null)
            {
                TrykkflateMm = model.TrykkflateMm;
                PalastHastighetMPas = model.PalastHastighetMPas;
                TrykkfasthetMPa = model.TrykkfasthetMPa;
                TrykkfasthetMPaNSE = model.TrykkfasthetMPaNSE;
                _reportModelId = model.ReportModelId;
            }
        }

        // Commands to handle UI actions

        [RelayCommand]
        public void Submit()
        {
            // Create a new entry model using the current state of the ViewModel
            var newEntry = new TrykktestingModel(
                Guid.NewGuid(), // Assuming a new unique identifier for each new record
                TrykkflateMm,
                PalastHastighetMPas,
                TrykkfasthetMPa,
                TrykkfasthetMPaNSE,
                _reportModelId);

            // No additional logic for creating a new instance of TrykktestingPrøveVM needed here

            // You may want to handle adding the new entry to a collection in another ViewModel here
            // or handle it in a different part of your application flow

            // Create a new instance of DataFraOppdragsgiverPrøverVM for the new entry
            var tryyketest = new TrykktestingPrøveVM(newEntry)
            {
                // If other properties need to be set directly, set them here
                // Additional properties set up could be placed here if needed
            };

            // Add the new Prøve VM to the collection in DataFraOppdragsgiverTableVM
            _trykktestingTableVM.Trykketester.Add(tryyketest);


            _modalNavigation.Close();
            // Optionally, handle modal navigation or UI feedback
        }


        [RelayCommand]
        public void Cancel()
        {
            _modalNavigation.Close();
        }
    }
}
