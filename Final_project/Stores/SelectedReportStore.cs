using Domain.Models;

namespace Final_project.Stores
{
    public class SelectedReportStore
    {

        public ReportStore _reportStore;
        private ReportModel _selectedReport;

        public SelectedReportStore(ReportStore reportStore)
        {
            _reportStore = reportStore;

            _reportStore.ReportUpdated += _reportStore_ReportUpdated;
        }

        private void _reportStore_ReportUpdated(ReportModel reportModel)
        {

            if (reportModel != null)
            {
                SelectedReport = reportModel;
            }
        }




        public ReportModel SelectedReport
        {
            get { return _selectedReport; }
            set
            {
                _selectedReport = value;
                OnSelectedReportChanged();
            }
        }

        public event Action SelectedReportChanged;

        protected virtual void OnSelectedReportChanged()
        {
            SelectedReportChanged?.Invoke();
        }
    }
}
