using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    internal class HomeVM : ViewModelBase
    {
        public ReportDetailsVM ReportDetailsVM { get; }

        public ReportListVM ReportListVM { get; }
        public ICommand AddReportCommand { get; }

        public HomeVM(SelectedReportStore _selectedReportStore)
        {
            ReportDetailsVM = new ReportDetailsVM(_selectedReportStore);
            ReportListVM = new ReportListVM(_selectedReportStore);

        }

    }
}
