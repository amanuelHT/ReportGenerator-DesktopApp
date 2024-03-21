using Final_project.Stores;

namespace Final_project.Commands
{
    public class NavigateHomeCommand : CommandBase
    {
        ModalNavigation _navigationStore;

        public NavigateHomeCommand(ModalNavigation navigationStore)
        {
            _navigationStore = navigationStore;

        }
        public override void Execute(object parameter)
        {
            //_navigationStore.NavigateTo<HomeVM>();

        }
    }
}
