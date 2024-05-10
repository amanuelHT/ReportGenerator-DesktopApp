using Final_project.Service;
using Final_project.Stores;

namespace Final_project.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly AccountStore _accountStore;
        private readonly INavigationService _homeNavigationService;

        public LogoutCommand(AccountStore accountStore, INavigationService homeNavigationService)
        {
            _accountStore = accountStore;
            _homeNavigationService = homeNavigationService;
        }

        public override void Execute(object parameter)
        {
            _accountStore.Logout();
            _homeNavigationService.Navigate();
        }
    }
}
