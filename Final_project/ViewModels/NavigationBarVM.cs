
using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using System.Windows;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class NavigationBarVM : ObservableObject, IDisposable
    {
        private readonly AccountStore _accountStore;
        private object createSettingsNavigarionService;
        private readonly INavigationService _navigationService;

        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public bool IsLoggedOut => _accountStore.IsLoggedOut;

        public ICommand NavigateSettingsCommand { get; }
        public ICommand NavigateKundeServiceCommand { get; }
        public ICommand NavigateAccountCommand { get; }
        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigateGeneratedReportListingCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand NavigateReportViewerCommand { get; }
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateRoleManagementViewCommand { get; private set; }
        public ICommand NavigateRoleManagementCommand { get; }
        public ICommand NavigateUserInfoCommand { get; }

        public NavigationBarVM(
                AccountStore accountStore,
                INavigationService settingsNavigationService,
                INavigationService accountNavigationService,
                INavigationService loginNavigationService,
                INavigationService generatedReportListingNavigationService,
                INavigationService reportViewernavigarionService,
                INavigationService HomeNavigationService,
                INavigationService roleManagementNavigationService,
                INavigationService UserInfoNavigationService,
                INavigationService navigateKundeServiceCommand)
        {
            _accountStore = accountStore;
            _navigationService = roleManagementNavigationService;

            NavigateSettingsCommand = new NavigateCommand(settingsNavigationService);
            NavigateAccountCommand = new NavigateCommand(accountNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            NavigateGeneratedReportListingCommand = new NavigateCommand(generatedReportListingNavigationService);
            NavigateReportViewerCommand = new NavigateCommand(reportViewernavigarionService);
            NavigateHomeCommand = new NavigateCommand(HomeNavigationService);
            LogoutCommand = new LogoutCommand(_accountStore, HomeNavigationService);
            NavigateRoleManagementViewCommand = new NavigateCommand(roleManagementNavigationService);
            NavigateUserInfoCommand = new NavigateCommand(UserInfoNavigationService);
            NavigateKundeServiceCommand = new NavigateCommand(navigateKundeServiceCommand

                 );


            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;


            NavigateRoleManagementCommand = new RelayCommand(
            p => _navigationService.Navigate(),
            p => _accountStore.CurrentAccount?.Role == "Admin");


        }
        public Visibility RoleManagementVisibility =>
            _accountStore.CurrentAccount?.Role == "Admin" ? Visibility.Visible : Visibility.Collapsed;

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(IsLoggedOut));
            OnPropertyChanged(nameof(RoleManagementVisibility));
        }

        public void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
        }


    }
}
