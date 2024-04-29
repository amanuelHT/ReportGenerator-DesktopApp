using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels.TablesVM
{
    public partial class DataEtterKuttingOgSlipingTableVM : ObservableObject
    {

        private readonly DataEtterKuttingOgSlipingPrøveVM _pr;
        public ObservableCollection<DataEtterKuttingOgSlipingPrøveVM> Prøver { get; }


        public ModalNavigation _modalNavigation { get; }
        public Guid _reportid { get; }

        public DataEtterKuttingOgSlipingTableVM(ModalNavigation modalNavigation, Guid reportid)
        {



            Prøver = new ObservableCollection<DataEtterKuttingOgSlipingPrøveVM>();
            _modalNavigation = modalNavigation;
            _reportid = reportid;
        }


        [RelayCommand]
        private void AddPrøve(DataEtterKuttingOgSlipingPrøveVM prøve)
        {

            DataEtterKuttingOgSlipingPrøveVM dataFraOppdragsgiverPrøverVM = new DataEtterKuttingOgSlipingPrøveVM(_modalNavigation, _reportid, this);
            _modalNavigation.CurrentView = dataFraOppdragsgiverPrøverVM;


        }



        [RelayCommand]
        private void RemoveRecord(DataEtterKuttingOgSlipingPrøveVM prøve)
        {
            if (prøve != null && Prøver.Contains(prøve))
            {
                Prøver.Remove(prøve);
            }
        }

    }
}
