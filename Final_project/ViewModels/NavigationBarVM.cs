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

        public ICommand NavigateGeneratedReportListingCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand NavigateReportViewerCommand { get; }
        public ICommand NavigatHomeCommand { get; }
        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public ICommand NavigateRoleManagementViewCommand { get; private set; }

        public NavigationBarVM(
            AccountStore accountStore,
            INavigationService settingsNavigationService,
            INavigationService accountNavigationService,
            INavigationService loginNavigationService,
            INavigationService generatedReportListingNavigationService,
            INavigationService reportViewernavigarionService,
            INavigationService HomeNavigationService,
            INavigationService roleManagementNavigationService)
        {
            _accountStore = accountStore;
            NavigateSettingsCommand = new NavigateCommand(settingsNavigationService);
            NavigateAccountCommand = new NavigateCommand(accountNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            NavigateGeneratedReportListingCommand = new NavigateCommand(generatedReportListingNavigationService);
            NavigateReportViewerCommand = new NavigateCommand(reportViewernavigarionService);
            NavigatHomeCommand = new NavigateCommand(HomeNavigationService);
            LogoutCommand = new LogoutCommand(_accountStore);
            NavigateRoleManagementViewCommand = new NavigateCommand(roleManagementNavigationService);

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
