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
        private ObservableCollection<ReportImageModel> _reportImages;
        private readonly ReportStore _reportStore;
        public ICommand NavigateGeneratedReportCommand { get; }
        public ObservableCollection<ReportModel> AvailableReports { get; }

        private ReportModel _selectedReport;
        public ReportModel SelectedReport
        {
            get => _selectedReport;
            set
            {
                _selectedReport = value;
                OnPropertyChanged(nameof(SelectedReport));
                // Trigger loading of the data for the selected report
                LoadSelectedReport();
            }
        }

        public ReportViewerVM(ReportStore reportStore, INavigationService generatedReportNavigationService)
        {
            _reportStore = reportStore;
            _reports = new ObservableCollection<ReportModel>();
            LoadReports(); // Load all reports initially
            NavigateGeneratedReportCommand = new NavigateCommand(generatedReportNavigationService);
            AvailableReports = new ObservableCollection<ReportModel>(_reportStore.ReportModels);
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

        // Add a property to expose the selected report's data
        public ReportModel SelectedReportData { get; private set; }

        private async Task LoadSelectedReport()
        {
            if (_selectedReport == null) return;

            var selectedReportData = await _reportStore.GetReportData(_selectedReport.Id);
            if (selectedReportData != null)
            {
                SelectedReportData = selectedReportData; // Set the selected report's data
                OnPropertyChanged(nameof(SelectedReportData)); // Notify the view
            }
        }

    }
}

