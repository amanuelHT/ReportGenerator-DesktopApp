using Domain.Models;
using Final_project.Stores;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels
{
    public class ReportViewerVM : ViewModelBase
    {
        private ObservableCollection<ReportModel> _reports;
        private readonly ReportStore _reportStore;

        public ObservableCollection<ReportModel> Reports => _reports;

        public ReportViewerVM(ReportStore reportStore)
        {
            _reportStore = reportStore;
            _reports = new ObservableCollection<ReportModel>();
            LoadReports();
        }

        private async Task LoadReports()
        {
            await _reportStore.Load();
            _reports.Clear();
            foreach (var report in _reportStore.ReportModels)
            {
                _reports.Add(report);
            }
        }
    }
}
