using Domain.Models;
using Final_project.Stores;

namespace Final_project.ViewModels
{
    public class ReportDetailsVM : ViewModelBase
    {
        private readonly SelectedReportStore _selectedReportStore;
        private ReportModel SelectedReport => _selectedReportStore.SelectedReport;


        public bool HasReportSelected => SelectedReport != null;


        public string Tittle => SelectedReport?.Tittle ?? "unkowm";
        public string Status => (SelectedReport?.Status ?? false) ? "Godkjent" : "Ikke Godkjent";
        public string Kunde => SelectedReport?.Kunde ?? "no name ";
        public ReportDetailsVM(SelectedReportStore selectedReportStore)
        {

            _selectedReportStore = selectedReportStore;
            _selectedReportStore.SelectedReportChanged += _selectedReportStore_SelectedReportChanged;
        }

        protected override void Dispose()
        {
            _selectedReportStore.SelectedReportChanged -= _selectedReportStore_SelectedReportChanged;

            base.Dispose();
        }

        private void _selectedReportStore_SelectedReportChanged()
        {
            OnPropertyChanged(nameof(HasReportSelected));
            OnPropertyChanged(nameof(Tittle));
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(Kunde));
        }
    }
}
