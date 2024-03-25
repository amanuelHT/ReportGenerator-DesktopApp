using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class GeneratedReportListVM : ViewModelBase
    {
        private readonly GeneratedReportStore _generatedReportStore;

        private readonly ObservableCollection<GeneratedReportVM> _generatedReport;

        public IEnumerable<GeneratedReportVM> GeneratedReport => _generatedReport;

        public ICommand command { get; }
        public GeneratedReportListVM(GeneratedReportStore peopleStore, INavigationService navigationService)
        {
            command = new NavigateCommand(navigationService);

            _generatedReportStore = peopleStore;

            _generatedReport = new ObservableCollection<GeneratedReportVM>();

            _generatedReport.Add(new GeneratedReportVM("Report 1"));
            _generatedReport.Add(new GeneratedReportVM("Report 2"));
            _generatedReport.Add(new GeneratedReportVM("Report 3"));

            _generatedReportStore.ReportAdded += OnPersonAdded;
        }

        private void OnPersonAdded(string name)
        {
            _generatedReport.Add(new GeneratedReportVM(name));
        }
    }
}
