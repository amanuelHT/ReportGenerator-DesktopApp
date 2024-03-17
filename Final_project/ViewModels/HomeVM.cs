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

        public HomeVM(ReportStore reportStore, SelectedReportStore selectedReportStore, NavigationStore navigationStore)
        {
            ReportDetailsVM = new ReportDetailsVM(selectedReportStore);
            ReportListVM = ReportListVM.loadViewModel(reportStore, selectedReportStore, navigationStore);


            AddReportCommand = new OpenAddCommand(reportStore, navigationStore);

        }

    }
}