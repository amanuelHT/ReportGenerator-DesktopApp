using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class HomeVM : ViewModelBase
    {

        public ReportDetailsVM ReportDetailsVM { get; }

        public ReportListVM ReportListVM { get; }
        public ICommand AddReportCommand { get; }

        public HomeVM(SelectedReportStore _selectedReportStore, NavigationStore navigationStore)
        {
            ReportDetailsVM = new ReportDetailsVM(_selectedReportStore);
            ReportListVM = new ReportListVM(_selectedReportStore);

            AddReportCommand = new OpenAddCommand(navigationStore);

        }

    }
}
