using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{
    public class OpenAddCommand : CommandBase
    {
        private readonly ReportStore _reportStore;
        private readonly NavigationStore _navigationStore;

        public OpenAddCommand(ReportStore reportStore, NavigationStore navigationStore)
        {
            _reportStore = reportStore;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            AddReportVM addReportVM = new AddReportVM(_reportStore, _navigationStore);
            _navigationStore.CurrentView = addReportVM;
        }
    }
}