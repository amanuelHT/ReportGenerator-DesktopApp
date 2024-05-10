using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Stores;
using Report_Generator_Domain.Models;



namespace Final_project.ViewModels.TablesVM
{
    public partial class DataEtterKuttingOgSlipingPrøveVM : ObservableObject
    {
        [ObservableProperty]
        private Guid prøvenr;

        [ObservableProperty]
        private DateOnly ivannbadDato;

        [ObservableProperty]
        private DateOnly testDato;

        [ObservableProperty]
        private string overflatetilstand;

        [ObservableProperty]
        private double dm;

        [ObservableProperty]
        private double prøvetykke;

        [ObservableProperty]
        private double dmPrøvetykkeRatio;

        [ObservableProperty]
        private double trykkfasthetMPa;

        [ObservableProperty]
        private string fasthetSammenligning;

        [ObservableProperty]
        private double førSliping;

        [ObservableProperty]
        private double etterSliping;

        [ObservableProperty]
        private double mmTilTopp;


        private readonly ModalNavigation _modalNavigation;
        private readonly Guid _reportmodelid;
        private readonly DataEtterKuttingOgSlipingTableVM _dataEtterSlipingTableVM;

        public DataEtterKuttingOgSlipingPrøveVM(ModalNavigation modalNavigation, Guid reportmodelid, DataEtterKuttingOgSlipingTableVM dataEtterKuttingOgSlipingTableVM)
        {
            _modalNavigation = modalNavigation;
            _reportmodelid = reportmodelid;
            _dataEtterSlipingTableVM = dataEtterKuttingOgSlipingTableVM;
        }


        public DataEtterKuttingOgSlipingPrøveVM(DataEtterKuttingOgSlipingModel dataEtterKuttingOgSlipingModel)
        {
            if (dataEtterKuttingOgSlipingModel != null)
            {
                SetProperty(ref prøvenr, dataEtterKuttingOgSlipingModel.Id);
                SetProperty(ref ivannbadDato, dataEtterKuttingOgSlipingModel.IvannbadDato);
                SetProperty(ref testDato, dataEtterKuttingOgSlipingModel.TestDato);
                SetProperty(ref overflatetilstand, dataEtterKuttingOgSlipingModel.Overflatetilstand);
                SetProperty(ref dm, dataEtterKuttingOgSlipingModel.Dm);
                SetProperty(ref prøvetykke, dataEtterKuttingOgSlipingModel.Prøvetykke);
                SetProperty(ref dmPrøvetykkeRatio, dataEtterKuttingOgSlipingModel.DmPrøvetykkeRatio);
                SetProperty(ref trykkfasthetMPa, dataEtterKuttingOgSlipingModel.TrykkfasthetMPa);
                SetProperty(ref fasthetSammenligning, dataEtterKuttingOgSlipingModel.FasthetSammenligning);
                SetProperty(ref førSliping, dataEtterKuttingOgSlipingModel.FørSliping);
                SetProperty(ref etterSliping, dataEtterKuttingOgSlipingModel.EtterSliping);
                SetProperty(ref mmTilTopp, dataEtterKuttingOgSlipingModel.MmTilTopp);
            }
        }



        [RelayCommand]
        public virtual void Submit()
        {

            // Create a new entry model using the current state of the ViewModel
            var newEntry = new DataEtterKuttingOgSlipingModel(
              Guid.NewGuid(),
              this.ivannbadDato,
              this.testDato,
              this.overflatetilstand,
              this.dm,
               this.prøvetykke,
               this.DmPrøvetykkeRatio,
               this.trykkfasthetMPa,
               this.fasthetSammenligning,
               this.førSliping,
              this.etterSliping,
              this.mmTilTopp,
              _reportmodelid


            );


            // Create a new instance of DataFraOppdragsgiverPrøverVM for the new entry
            var newPrøveVM = new DataEtterKuttingOgSlipingPrøveVM(newEntry)
            {
                // If other properties need to be set directly, set them here
                // Additional properties set up could be placed here if needed
            };

            // Add the new Prøve VM to the collection in DataFraOppdragsgiverTableVM
            _dataEtterSlipingTableVM.Prøver.Add(newPrøveVM);



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