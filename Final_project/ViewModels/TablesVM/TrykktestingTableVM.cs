using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels.TablesVM
{
    public partial class TrykktestingTableVM : ObservableObject
    {

        private readonly TrykktestingPrøveVM _pr;
        public ObservableCollection<TrykktestingPrøveVM> Trykketester { get; }


        public ModalNavigation _modalNavigation { get; }
        public Guid _reportid { get; }

        public TrykktestingTableVM(ModalNavigation modalNavigation, Guid reportid)
        {



            Trykketester = new ObservableCollection<TrykktestingPrøveVM>();
            _modalNavigation = modalNavigation;
            _reportid = reportid;
        }


        [RelayCommand]
        public void AddRecord()
        {

            var trykktestingPrøveVM = new TrykktestingPrøveVM(_modalNavigation, _reportid, this);
            _modalNavigation.CurrentView = trykktestingPrøveVM;


        }



        [RelayCommand]
        public void RemovePrøve(TrykktestingPrøveVM prøve)
        {
            if (prøve != null && Trykketester.Contains(prøve))
            {
                Trykketester.Remove(prøve);
            }
        }

    }
}
