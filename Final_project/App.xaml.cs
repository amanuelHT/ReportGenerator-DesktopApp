using Final_project.Service;
using Final_project.Stores;
using Final_project.ViewModels;
using System.Windows;

namespace Final_project
{

    public partial class App : Application
    {

        private readonly AccountStore _accountStore;
        private readonly NavigationStore _navigationStore;

        public App()
        {

            _navigationStore = new NavigationStore();
            _accountStore = new AccountStore();

        }



        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);



            INavigationService settingsnavigationService = SettingsNavigarionService();
            settingsnavigationService.Navigate();

            //navigationStore.CurrentViewModel = new SettingsVM(_accountStore,_navigationStore);




            // Set up the main window
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)

            };
            MainWindow.Show();
        }


        private NavigationBarVM CreateNavigationBarViewModel()
        {

            return new NavigationBarVM(
                   _accountStore,
                  SettingsNavigarionService(),
                    AccountNavigarionService(),
                      LoginNavigarionService());
        }


        private INavigationService SettingsNavigarionService()
        {
            return new LayoutNavigationService<SettingsVM>(
                _navigationStore,
                () => new SettingsVM(LoginNavigarionService()),
                CreateNavigationBarViewModel);
        }

        private INavigationService LoginNavigarionService()
        {
            return new LayoutNavigationService<LoginVM>(
                   _navigationStore,
                   () => new LoginVM(_accountStore, AccountNavigarionService()),
                   CreateNavigationBarViewModel);

        }


        private INavigationService AccountNavigarionService()
        {
            return new LayoutNavigationService<AccountVM>(
               _navigationStore,
               () => new AccountVM(_accountStore, SettingsNavigarionService()),
               CreateNavigationBarViewModel);


        }
    }



    //public partial class App : Application
    //{

    //    public IServiceProvider ServiceProvider { get; private set; }


    //    private readonly IHost _host;
    //    public App()
    //    {
    //        _host = Host.CreateDefaultBuilder()
    //            .ConfigureServices((context, service) =>
    //            {

    //                // Configures and registers a SQLite database connection for ReportModelDbContext using dependency injection.
    //                string? connectionString = context.Configuration.GetConnectionString("Sqlite");
    //                service.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
    //                service.AddSingleton<ReportModelDbContextFactory>();



    //                service.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => type =>
    //                (ViewModelBase)serviceProvider.GetRequiredService(type));




    //                //navigation service 
    //                //service.AddSingleton<INavigation, Navigation>();


    //                //Queries and commands for database services
    //                service.AddSingleton<IGetAllReportsQuery, GetAllReportsQuery>();
    //                service.AddSingleton<ICreateReportCommand, CreateReportCommand>();
    //                service.AddSingleton<IDeleteReportCommand, DeleteReportCommand>();
    //                service.AddSingleton<IUpdateReportCommand, UpdateReportCommand>();


    //                //stores , Single source of truth, defnitly Singlton
    //                service.AddSingleton<ReportStore>();
    //                service.AddSingleton<SelectedReportStore>();
    //                service.AddSingleton<ModalNavigation>();


    //                //viewmodels 
    //                service.AddSingleton<MainViewModel>();
    //                service.AddTransient<HomeVM>(CreateHomeViewModel);
    //                service.AddSingleton<ReportViewerVM>();
    //                service.AddSingleton<EditReportVM>();
    //                service.AddSingleton<AddReportVM>();
    //                service.AddSingleton<ReportDetailsVM>();
    //                service.AddSingleton<ReportFormVM>();
    //                service.AddSingleton<ReportListingItemVM>();
    //                service.AddSingleton<ReportListVM>();
    //                service.AddSingleton<ViewModelBase>();


    //                //Views 
    //                service.AddSingleton<AddReportView>();
    //                service.AddSingleton<EditReportView>();
    //                service.AddSingleton<HomeView>();
    //                service.AddSingleton<ReportViewerView>();



    //                //navigation
    //                service.AddSingleton<Func<Type, ViewModelBase>>(service => T => (ViewModelBase)service.GetService(typeof(ViewModelBase)));


    //                //Components view
    //                service.AddSingleton<ReportDetailsView>();
    //                service.AddSingleton<ReportForm>();
    //                service.AddSingleton<ReportListingItemVM>();
    //                service.AddSingleton<ReportListView>();

    //                service.AddSingleton<MainWindow>((services) => new MainWindow()
    //                {
    //                    DataContext = services.GetRequiredService<MainViewModel>()
    //                });



    //                Bold.Licensing.BoldLicenseProvider.RegisterLicense
    //                (
    //                     context.Configuration.GetValue<string>("Licensekey")
    //                 );



    //            })
    //            .Build();







    //    }

    //    private HomeVM CreateHomeViewModel(IServiceProvider service)
    //    {
    //        return HomeVM.LoadHome(
    //             service.GetRequiredService<ReportStore>(),
    //             service.GetRequiredService<SelectedReportStore>(),
    //             service.GetRequiredService<ModalNavigation>());
    //    }

    //    protected override void OnStartup(StartupEventArgs e)
    //    {
    //        _host.Start();
    //        ServiceProvider = _host.Services;


    //        ReportModelDbContextFactory reportModelDbContextFactory =
    //             _host.Services.GetRequiredService<ReportModelDbContextFactory>();
    //        using (ReportModelDbContext context = reportModelDbContextFactory.Create())

    //        {
    //            context.Database.Migrate();

    //        }


    //        MainWindow = _host.Services.GetRequiredService<MainWindow>();
    //        MainWindow.Show();
    //        // MainWindow.Show();



    //        base.OnStartup(e);
    //    }

    //    protected override void OnExit(ExitEventArgs e)
    //    {
    //        _host.StopAsync();
    //        _host.Dispose();
    //    }
    //}
}


