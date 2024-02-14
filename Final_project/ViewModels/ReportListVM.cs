using Final_project.Commands;
using Final_project.Models;
using Final_project.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    // ViewModel responsible for managing a list of reports
    public class ReportListVM : ViewModelBase
    {

        private readonly SelectedReportStore _selectedReportStore;
        private readonly ObservableCollection<ReportListingItemVM> _reportListingItemVM;
        public IEnumerable<ReportListingItemVM> ReportListingItemVM => _reportListingItemVM;

        private ReportListingItemVM _selectedReportListingItemVM;
        private readonly NavigationStore navigationStore;

        public ReportListingItemVM SelectedReportListingItemVM
        {
            get
            {
                return _selectedReportListingItemVM;
            }
            set
            {
                _selectedReportListingItemVM = value;
                OnPropertyChanged(nameof(SelectedReportListingItemVM));

                _selectedReportStore.SelectedReport = _selectedReportListingItemVM?.ReportModel;
            }


        }

        // Constructor initializes the report list and adds sample reports
        public ReportListVM(SelectedReportStore selectedReportStore, NavigationStore navigationStore)
        {
            _selectedReportStore = selectedReportStore;
            // Initialize the collection of report ViewModels
            _reportListingItemVM = new ObservableCollection<ReportListingItemVM>();

            // Add sample reports to the collection
            AddReport(new ReportModel("report1", true, "the ove"), navigationStore);
            AddReport(new ReportModel("report2", true, "killer"), navigationStore);
            AddReport(new ReportModel("report3", true, "none"), navigationStore);
            AddReport(new ReportModel("report4", true, "jok"), navigationStore);
            AddReport(new ReportModel("report5", true, "water"), navigationStore);
            AddReport(new ReportModel("report6", true, "free"), navigationStore);
        }

        private void AddReport(ReportModel reportModel, NavigationStore navigationStore)
        {
            ICommand editCommand = new OpenEditCommand(reportModel, navigationStore);
            _reportListingItemVM.Add(new ReportListingItemVM(reportModel, editCommand));

        }
    }
}


// +----------------------------------------+
// |               Final_project            |
// +----------------------------------------+
//         |
//         |
// +----------------------------------+
// |           ViewModels            |
// +----------------------------------+
//         |
//         |
// +-----------------------------------+
// |         ReportListVM              |
// +------------------+----------------+
//                    |
//                    |
// +---------------------------+---------------------+
// |                                                |
// | ReportListingItemVM("Report1")                 |  
// +-------------------+                            |
// | ReportListingItemVM("Report1")                 |
// +-------------------+-------------------------+--+
//          |                                     |
//          |                                     |
// +------------------+                           |
// | SelectedReportStore                         |
// +------------------+                           |
// |   _selectedReport                          |
// +------------------+
