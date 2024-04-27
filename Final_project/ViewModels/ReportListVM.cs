using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;



namespace Final_project.ViewModels
{

    public class ReportListVM : ViewModelBase
    {

        private readonly SelectedReportStore _selectedReportStore;


        private readonly ObservableCollection<ReportListingItemVM> _reportListingItemVM;
        public IEnumerable<ReportListingItemVM> ReportListingItemVM => _reportListingItemVM;


        private ReportListingItemVM _selectedReportListingItemVM;
        private readonly ModalNavigation _modalNavigation;
        private readonly NavigationStore _navigationStore;
        private readonly ReportStore _reportStore;


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


        ICommand LoadReportCommand { get; }

        // Constructor initializes the report list and adds sample reports
        public ReportListVM(ReportStore reportStore, SelectedReportStore selectedReportStore, NavigationStore navigationStore, ModalNavigation modalNavigation)
        {
            _reportStore = reportStore;
            _selectedReportStore = selectedReportStore;
            _navigationStore = navigationStore;
            _modalNavigation = modalNavigation;
            _reportListingItemVM = new ObservableCollection<ReportListingItemVM>();

            _reportStore.ReportAdded += ReportStore_ReportStoreAdded;
            _reportStore.ReportUpdated += ReportStore_ReportStoreUpdated;
            _reportStore.ReportModelLoaded += ReportStore_ReportModelLoaded;
            _reportStore.ReportDeleted += ReportStore_Deleted;

            LoadReportCommand = new LoadReportCommand(reportStore);

        }

        private void ReportStore_ReportModelLoaded()
        {
            _reportListingItemVM.Clear();
            foreach (ReportModel reportModel in _reportStore.ReportModels)
            {
                AddReport(reportModel);
            }
        }

        public static ReportListVM loadViewModel(ReportStore reportStore, SelectedReportStore selectedReportStore, NavigationStore navigationStore, ModalNavigation modalNavigation)

        {
            ReportListVM viewmodel = new ReportListVM(reportStore, selectedReportStore, navigationStore, modalNavigation);
            viewmodel.LoadReportCommand.Execute(null);
            return viewmodel;
        }

        private void ReportStore_ReportStoreAdded(ReportModel reportModlel)
        {
            AddReport(reportModlel);
        }


        private void ReportStore_ReportStoreUpdated(ReportModel ReportModel)
        {

            ReportListingItemVM report = _reportListingItemVM.FirstOrDefault(y => y.ReportModel.Id == ReportModel.Id);
            if (report != null)
            {
                report.UpdateReport(ReportModel);

            }

        }
        private void ReportStore_Deleted(Guid id)
        {
            ReportListingItemVM itemViewModel = _reportListingItemVM.FirstOrDefault(y => y.ReportModel?.Id == id);

            if (itemViewModel != null)
            {
                _reportListingItemVM.Remove(itemViewModel);
            }
        }



        public override void Dispose()
        {
            _reportStore.ReportAdded -= ReportStore_ReportStoreAdded;
            _reportStore.ReportUpdated -= ReportStore_ReportStoreUpdated;
            _reportStore.ReportModelLoaded -= ReportStore_ReportModelLoaded;
            _reportStore.ReportDeleted -= ReportStore_Deleted;

            base.Dispose();
        }



        private void AddReport(ReportModel reportModel)
        {
            ReportListingItemVM itemVM = new ReportListingItemVM(reportModel, _reportStore, _navigationStore, _modalNavigation);
            _reportListingItemVM.Add(itemVM);

        }



    }
}
