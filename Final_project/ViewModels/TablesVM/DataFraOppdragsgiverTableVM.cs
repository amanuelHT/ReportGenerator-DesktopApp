using CommunityToolkit.Mvvm.ComponentModel;
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
        public ICommand RemovePrøveCommand { get; }

        public DataFraOppdragsgiverTableVM(ReportStore reportStore, Guid reportId)
        {
            _reportStore = reportStore;
            _reportId = reportId;

            Prøver = new ObservableCollection<DataFraOppdragsgiverPrøverVM>();
            //_pr = new DataFraOppdragsgiverPrøverVM(reportStore, reportId, this);


        }


    }
}
