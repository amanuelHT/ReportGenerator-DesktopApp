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

        public ReportModel SelectedReportData { get; private set; }
        public ObservableCollection<ReportImageModel> ReportImages { get; private set; }

        public ReportViewerVM(ReportStore reportStore, INavigationService generatedReportNavigationService)
        {
            _reportStore = reportStore;
            NavigateGeneratedReportCommand = new NavigateCommand(generatedReportNavigationService);
            AvailableReports = new ObservableCollection<ReportModel>(_reportStore.ReportModels);
        }

        private async Task LoadSelectedReport()
        {
            if (_selectedReport == null) return;

            // Retrieve the full report data with images
            (ReportModel reportData, List<ReportImageModel> images) = await _reportStore.GetReportData(_selectedReport.Id);

            if (reportData != null)
            {
                SelectedReportData = reportData; // Set the selected report's data
                OnPropertyChanged(nameof(SelectedReportData)); // Notify the view

                // Load images for the selected report
                ReportImages = new ObservableCollection<ReportImageModel>(images);
                OnPropertyChanged(nameof(ReportImages)); // Notify the view
            }
        }
    }
}
