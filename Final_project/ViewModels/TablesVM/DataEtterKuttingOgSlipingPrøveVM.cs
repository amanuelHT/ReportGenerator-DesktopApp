using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Stores;
using Report_Generator_Domain.Models;



namespace Final_project.ViewModels.TablesVM
{
    public partial class DataEtterKuttingOgSlipingPrøveVM : ObservableObject
    {

        private Guid id;


        [ObservableProperty]
        private DateTime ivannbadDato;


        [ObservableProperty]
        private DateTime testDato;



        [ObservableProperty]
        private int prøvenr;

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
            testDato = DateTime.Now;
            ivannbadDato = DateTime.Now;
        }


        public DataEtterKuttingOgSlipingPrøveVM(DataEtterKuttingOgSlipingModel dataEtterKuttingOgSlipingModel)
        {
            if (dataEtterKuttingOgSlipingModel != null)
            {

                testDato = dataEtterKuttingOgSlipingModel.TestDato;
                ivannbadDato = dataEtterKuttingOgSlipingModel.IvannbadDato;
                prøvenr = dataEtterKuttingOgSlipingModel.Prøvenr;
                overflatetilstand = dataEtterKuttingOgSlipingModel.Overflatetilstand;
                dm = dataEtterKuttingOgSlipingModel.Dm;
                prøvetykke = dataEtterKuttingOgSlipingModel.Prøvetykke;
                dmPrøvetykkeRatio = dataEtterKuttingOgSlipingModel.DmPrøvetykkeRatio;
                trykkfasthetMPa = dataEtterKuttingOgSlipingModel.TrykkfasthetMPa;
                fasthetSammenligning = dataEtterKuttingOgSlipingModel.FasthetSammenligning;
                førSliping = dataEtterKuttingOgSlipingModel.FørSliping;
                etterSliping = dataEtterKuttingOgSlipingModel.EtterSliping;
                mmTilTopp = dataEtterKuttingOgSlipingModel.MmTilTopp;

            }
        }



        [RelayCommand]
        public virtual void Submit()
        {

            var newEntry = new DataEtterKuttingOgSlipingModel(
              Guid.NewGuid(),
              this.prøvenr,
              this.IvannbadDato.Date,
              this.TestDato.Date,
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


            var newPrøveVM = new DataEtterKuttingOgSlipingPrøveVM(newEntry)
            {
                // If other properties set here if it needed to be set directly, 
            };

            _dataEtterSlipingTableVM.Prøver.Add(newPrøveVM);


            _modalNavigation.Close();
        }

        [RelayCommand]
        public void Cancel()
        {

            _modalNavigation.Close();

        }

    }


}