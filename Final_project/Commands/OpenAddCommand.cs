using Final_project.Stores;
using Final_project.ViewModels;

namespace Final_project.Commands
{
    public class OpenAddCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;

        public OpenAddCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            AddReportVM addReportVM = new AddReportVM(_navigationStore);
            _navigationStore.CurrentView = addReportVM;
        }
    }
}