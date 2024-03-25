using Domain.Models;
using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ReportViewerVM : ViewModelBase
    {
        private ObservableCollection<ReportModel> _reports;
        private readonly ReportStore _reportStore;
        public ICommand NavigateGeneratedReportCommand { get; }

        public ObservableCollection<ReportModel> Reports => _reports;

        public ReportViewerVM(ReportStore reportStore, INavigationService generatedReportNavigationService)
        {

            _reportStore = reportStore;
            _reports = new ObservableCollection<ReportModel>();
            LoadReports();
            NavigateGeneratedReportCommand = new NavigateCommand(generatedReportNavigationService);

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
