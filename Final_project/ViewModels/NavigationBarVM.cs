    using Final_project.Commands;
    using Final_project.Service;
    using Final_project.Stores;
    using System.Windows.Input;
    using System.Windows;

    using System.Windows.Navigation;

    namespace Final_project.ViewModels
    {
        public class NavigationBarVM : ViewModelBase
        {
            private readonly AccountStore _accountStore;
            private object createSettingsNavigarionService;
            private readonly INavigationService _navigationService;

       
            public ICommand NavigateSettingsCommand { get; }
            public ICommand NavigateAccountCommand { get; }
            public ICommand NavigateLoginCommand { get; }
            public ICommand NavigateGeneratedReportListingCommand { get; }
            public ICommand LogoutCommand { get; }
            public ICommand NavigateReportViewerCommand { get; }
            public ICommand NavigatHomeCommand { get; }
            public bool IsLoggedIn => _accountStore.IsLoggedIn;
            public bool IsLoggedOut => _accountStore.IsLoggedOut;
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
                INavigationService UserInfoNavigationService)
            {
                _accountStore = accountStore;
                _navigationService = roleManagementNavigationService;

                NavigateSettingsCommand = new NavigateCommand(settingsNavigationService);
                NavigateAccountCommand = new NavigateCommand(accountNavigationService);
                NavigateLoginCommand = new NavigateCommand(loginNavigationService);
                NavigateGeneratedReportListingCommand = new NavigateCommand(generatedReportListingNavigationService);
                NavigateReportViewerCommand = new NavigateCommand(reportViewernavigarionService);
                NavigatHomeCommand = new NavigateCommand(HomeNavigationService);
                LogoutCommand = new LogoutCommand(_accountStore);
                NavigateRoleManagementViewCommand = new NavigateCommand(roleManagementNavigationService);
                NavigateUserInfoCommand = new NavigateCommand(UserInfoNavigationService);


                _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;


              NavigateRoleManagementCommand = new RelayCommand(
              p => _navigationService.Navigate(),
              p => _accountStore.CurrentAccount?.Role == "Admin");


        }
        // In NavigationBarVM
        public Visibility RoleManagementVisibility =>
            _accountStore.CurrentAccount?.Role == "Admin" ? Visibility.Visible : Visibility.Collapsed;

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(IsLoggedOut));
            OnPropertyChanged(nameof(RoleManagementVisibility));
        }

        public override void Dispose()
            {
                _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

                base.Dispose();
            }
        }
    }
