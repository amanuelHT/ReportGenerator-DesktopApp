using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Final_project.ViewModels.TablesVM
{
    public partial class DataFraOppdragsgiverTableVM : ObservableObject
    {
        private readonly ReportStore _reportStore;
        private readonly Guid _reportId;

        private readonly DataFraOppdragsgiverPrøverVM _pr;
        public ObservableCollection<DataFraOppdragsgiverPrøverVM> Prøver { get; }

        public ICommand AddPrøveCommand { get; }


        public DataFraOppdragsgiverTableVM(ReportStore reportStore, ModalNavigation modalNavigation, Guid reportId)
        {
            _reportStore = reportStore;
            _reportId = reportId;



            Prøver = new ObservableCollection<DataFraOppdragsgiverPrøverVM>();
            AddPrøveCommand = new OpenAddReportTableCommand(modalNavigation, reportStore, this, reportId);



            //_pr = new DataFraOppdragsgiverPrøverVM(reportStore, reportId, this);


        }



        [RelayCommand]
        private void RemovePrøve(DataFraOppdragsgiverPrøverVM prøve)
        {
            if (prøve != null && Prøver.Contains(prøve))
            {
                Prøver.Remove(prøve);
            }
        }



    }
}
