using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels.TablesVM
{
    public partial class ConcreteDensityTableVM : ObservableObject
    {

        private readonly ConcreteDensityPrøveVM _pr;
        public ObservableCollection<ConcreteDensityPrøveVM> Prøver { get; }


        public ModalNavigation _modalNavigation { get; }
        public Guid _reportid { get; }

        public ConcreteDensityTableVM(ModalNavigation modalNavigation, Guid reportid)
        {



            Prøver = new ObservableCollection<ConcreteDensityPrøveVM>();
            _modalNavigation = modalNavigation;
            _reportid = reportid;
        }


        [RelayCommand]
        private void AddRecord(ConcreteDensityPrøveVM prøve)
        {

            ConcreteDensityPrøveVM concreteDensityPrøveVM = new ConcreteDensityPrøveVM(_modalNavigation, _reportid, this);
            _modalNavigation.CurrentView = concreteDensityPrøveVM;


        }



        [RelayCommand]
        private void RemovePrøve(ConcreteDensityPrøveVM prøve)
        {
            if (prøve != null && Prøver.Contains(prøve))
            {
                Prøver.Remove(prøve);
            }
        }

    }
}
