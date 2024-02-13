using Final_project.Models;

namespace Final_project.Stores
{
    public class SelectedReportStore
    {
        private ReportModel _selectedReport;

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
