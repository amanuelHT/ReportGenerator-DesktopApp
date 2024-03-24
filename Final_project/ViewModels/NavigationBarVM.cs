using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class NavigationBarVM : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private object createSettingsNavigarionService;

        public ICommand NavigateSettingsCommand { get; }
        public ICommand NavigateAccountCommand { get; }
        public ICommand NavigateLoginCommand { get; }

        //public ICommand NavigatePeopleListingCommand { get; }
        public ICommand LogoutCommand { get; }

        public bool IsLoggedIn => _accountStore.IsLoggedIn;

        public NavigationBarVM(
            AccountStore accountStore,
            INavigationService settingsNavigationService,
            INavigationService accountNavigationService,
            INavigationService loginNavigationService)
        {
            _accountStore = accountStore;
            NavigateSettingsCommand = new NavigateCommand(settingsNavigationService);
            NavigateAccountCommand = new NavigateCommand(accountNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            LogoutCommand = new LogoutCommand(_accountStore);

            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }


        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

            base.Dispose();
        }
    }
}
