using Final_project.Models;
using Final_project.Stores;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels
{
    // ViewModel responsible for managing a list of reports
    class ReportListVM : ViewModelBase
    {

        private readonly SelectedReportStore _selectedReportStore;
        private readonly ObservableCollection<ReportListingItemVM> _reportListingItemVM;
        public IEnumerable<ReportListingItemVM> ReportListingItemVM => _reportListingItemVM;

        private ReportListingItemVM _selectedReportListingItemVM;
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

        public ReportListVM(SelectedReportStore selectedReportStore)
        {

            _selectedReportStore = selectedReportStore;

            // Initialize the collection of report ViewModels
            _reportListingItemVM = new ObservableCollection<ReportListingItemVM>();



            // Add sample reports to the collection
            _reportListingItemVM.Add(new ReportListingItemVM(new ReportModel("report1", true, "teklit")));
            _reportListingItemVM.Add(new ReportListingItemVM(new ReportModel("report2", false, "sirak")));
            _reportListingItemVM.Add(new ReportListingItemVM(new ReportModel("report3", true, "sami")));

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
