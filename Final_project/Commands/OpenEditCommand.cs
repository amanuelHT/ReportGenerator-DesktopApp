using Final_project.Models;
using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{
    public class OpenEditCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ReportModel _reportModel;

        public OpenEditCommand(ReportModel reportModel, NavigationStore navigationStore)
        {
            _reportModel = reportModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            EditReportVM editReportVM = new EditReportVM(_reportModel, _navigationStore);
            _navigationStore.CurrentView = editReportVM;
        }
    }
}
