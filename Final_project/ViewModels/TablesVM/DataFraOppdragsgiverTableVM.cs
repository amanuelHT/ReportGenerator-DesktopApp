using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels.TablesVM
{
    public partial class DataFraOppdragsgiverTableVM : ObservableObject
    {
        private readonly ReportStore _reportStore;
        private readonly Guid _reportId;

        private readonly DataFraOppdragsgiverPrøverVM _pr;
        public ObservableCollection<DataFraOppdragsgiverPrøverVM> Prøver { get; }

        public ModalNavigation _mmodalNavigation { get; }

        public DataFraOppdragsgiverTableVM(ReportStore reportStore, ModalNavigation modalNavigation, Guid reportId)
        {
            _reportStore = reportStore;
            _mmodalNavigation = modalNavigation;
            _reportId = reportId;



            Prøver = new ObservableCollection<DataFraOppdragsgiverPrøverVM>();

        }


        [RelayCommand]
        private void RemovePrøve(DataFraOppdragsgiverPrøverVM prøve)
        {
            if (prøve != null && Prøver.Contains(prøve))
            {
                Prøver.Remove(prøve);
            }
        }


        [RelayCommand]
        private void AddPrøve(DataFraOppdragsgiverPrøverVM prøve)
        {
            DataFraOppdragsgiverPrøverVM dataFraOppdragsgiverPrøverVM = new DataFraOppdragsgiverPrøverVM(_mmodalNavigation, _reportId, this);
            _mmodalNavigation.CurrentView = dataFraOppdragsgiverPrøverVM;

        }



    }
}
