using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Final_project.ViewModels
{
    public partial class ReportListVM : ObservableObject, IDisposable
    {
        private readonly SelectedReportStore _selectedReportStore;
        private readonly ObservableCollection<ReportListingItemVM> _reportListingItemVM;
        private readonly NavigationStore _navigationStore;
        private readonly HomeVM _homeVM;
        private readonly ReportStore _reportStore;

        [ObservableProperty]
        private ReportListingItemVM _selectedReportListingItemVM;

        public IEnumerable<ReportListingItemVM> ReportListingItemVM => _reportListingItemVM;


        public ICommand LoadReportCommand { get; }
        private bool _disposed = false;

        public ReportListVM(HomeVM homeVM, ReportStore reportStore, SelectedReportStore selectedReportStore, NavigationStore navigationStore)
        {
            _homeVM = homeVM;
            _reportStore = reportStore;
            _selectedReportStore = selectedReportStore;
            _navigationStore = navigationStore;
            _reportListingItemVM = new ObservableCollection<ReportListingItemVM>();

            _reportStore.ReportAdded += ReportStore_ReportStoreAdded;
            _reportStore.ReportUpdated += ReportStore_ReportStoreUpdated;
            _reportStore.ReportModelLoaded += ReportStore_ReportModelLoaded;
            _reportStore.ReportDeleted += ReportStore_Deleted;

            LoadReportCommand = new LoadReportCommand(reportStore);
        }

        public static ReportListVM loadViewModel(HomeVM homeVM, ReportStore reportStore, SelectedReportStore selectedReportStore, NavigationStore navigationStore)
        {
            ReportListVM viewmodel = new ReportListVM(homeVM, reportStore, selectedReportStore, navigationStore);
            viewmodel.LoadReportCommand.Execute(null);
            return viewmodel;
        }

        private void ReportStore_ReportModelLoaded()
        {
            _reportListingItemVM.Clear();
            foreach (ReportModel reportModel in _reportStore.ReportModels)
            {
                AddReport(reportModel);
            }
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

        private void AddReport(ReportModel reportModel)
        {
            ReportListingItemVM itemVM = new ReportListingItemVM(_homeVM, reportModel, _reportStore, _navigationStore);
            _reportListingItemVM.Add(itemVM);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Unsubscribe from events
                    _reportStore.ReportAdded -= ReportStore_ReportStoreAdded;
                    _reportStore.ReportUpdated -= ReportStore_ReportStoreUpdated;
                    _reportStore.ReportModelLoaded -= ReportStore_ReportModelLoaded;
                    _reportStore.ReportDeleted -= ReportStore_Deleted;
                }

                _disposed = true;
            }
        }
    }
}
