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


        [RelayCommand]
        public void Submit()
        {
            var newEntry = new TrykktestingModel(
                Guid.NewGuid(), 
                TrykkflateMm,
                PalastHastighetMPas,
                TrykkfasthetMPa,
                TrykkfasthetMPaNSE,
                _reportModelId);

            var tryyketest = new TrykktestingPrøveVM(newEntry)
            {
            };

            _trykktestingTableVM.Trykketester.Add(tryyketest);


            _modalNavigation.Close();
        }


        [RelayCommand]
        public void Cancel()
        {
            _modalNavigation.Close();
        }
    }
}
