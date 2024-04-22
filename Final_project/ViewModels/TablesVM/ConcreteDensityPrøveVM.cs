using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using Report_Generator_Domain.Models;

namespace Final_project.ViewModels.TablesVM
{
    public partial class ConcreteDensityPrøveVM : ObservableObject
    {

        private readonly ModalNavigation _modalNavigation;
        private Guid _reportModelId;
        private readonly ConcreteDensityTableVM _concreteDensityTableVM;
        [ObservableProperty]
        private int provnr;

        [ObservableProperty]
        private DateTime dato;

        [ObservableProperty]
        private double masseILuft;

        [ObservableProperty]
        private double masseIVannbad;

        [ObservableProperty]
        private double pw;

        [ObservableProperty]
        private double v;

        [ObservableProperty]
        private double densitet;

        // Constructor for new entries
        public ConcreteDensityPrøveVM(
            ModalNavigation modalNavigation,
            Guid reportid,
            ConcreteDensityTableVM concreteDensityTableVM)
        {

            _modalNavigation = modalNavigation;
            _reportModelId = reportid;
            _concreteDensityTableVM = concreteDensityTableVM;
        }

        // Constructor to populate from an existing model
        public ConcreteDensityPrøveVM(ConcreteDensityModel model)
        {
            if (model != null)
            {
                Provnr = model.Id;
                Dato = model.Dato;
                MasseILuft = model.MasseILuft;
                MasseIVannbad = model.MasseIVannbad;
                Pw = model.Pw;
                V = model.V;
                Densitet = model.Densitet;
                _reportModelId = model.ReportModelId;
            }
        }

        // Commands to handle UI actions




        [RelayCommand]
        public void Submit()
        {
            // Create a new entry model using the current state of the ViewModel
            var newEntry = new ConcreteDensityModel(
                Provnr,
                Dato,
                MasseILuft,
                MasseIVannbad,
                Pw,
                V,
                Densitet,
               _reportModelId // Assuming a new unique identifier for each new record
            );


            // Create a new instance of DataFraOppdragsgiverPrøverVM for the new entry
            var newPrøveVM = new ConcreteDensityPrøveVM(newEntry)
            {
                // If other properties need to be set directly, set them here
                // Additional properties set up could be placed here if needed
            };

            // Add the new Prøve VM to the collection in DataFraOppdragsgiverTableVM
            _concreteDensityTableVM.Prøver.Add(newPrøveVM);



            _modalNavigation.Close();
            // Optionally, handle modal navigation or UI feedback
            // _modalNavigation.CloseCurrent(); // Close the modal if that's part of your application flow
            // You may want to handle additional UI updates or confirmations here
        }


        [RelayCommand]
        public void Cancel()
        {
            _modalNavigation.Close();
        }
    }
}
