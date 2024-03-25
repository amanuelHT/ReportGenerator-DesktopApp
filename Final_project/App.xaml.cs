using Final_project.Service;
using Final_project.Stores;
using Final_project.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Final_project
{

    public partial class App : Application
    {
        private readonly IHost _host;
        public IServiceProvider _serviceProvider { get; private set; }

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, service) =>
             {

                 service.AddSingleton<AccountStore>();
                 service.AddSingleton<NavigationStore>();

                 service.AddSingleton<MainViewModel>();
                 service.AddSingleton<INavigationService>(s => SettingsNavigarionService(_serviceProvider));


                 service.AddSingleton<MainWindow>((services) => new MainWindow()
                 {
                     DataContext = services.GetRequiredService<MainViewModel>()
                 });



             })
               .Build();
            _serviceProvider = _host.Services;

        }



        protected override void OnStartup(StartupEventArgs e)
        {


            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();


            // Set up the main window
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);

        }


        private NavigationBarVM CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {

            return new NavigationBarVM(
                 serviceProvider.GetRequiredService<AccountStore>(),
                  SettingsNavigarionService(serviceProvider),
                    AccountNavigarionService(serviceProvider),
                      LoginNavigarionService(serviceProvider));
        }


        private INavigationService SettingsNavigarionService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<SettingsVM>(
               serviceProvider.GetRequiredService<NavigationStore>(),
                () => new SettingsVM(LoginNavigarionService(serviceProvider)),
                 () => CreateNavigationBarViewModel(serviceProvider));
        }

        private INavigationService LoginNavigarionService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<LoginVM>(
                  serviceProvider.GetRequiredService<NavigationStore>(),
                   () => new LoginVM(serviceProvider.GetRequiredService<AccountStore>(), AccountNavigarionService(serviceProvider)),
                   () => CreateNavigationBarViewModel(serviceProvider));

        }


        private INavigationService AccountNavigarionService(IServiceProvider serviceProvider)
        {
            {
                return new LayoutNavigationService<AccountVM>(
                  serviceProvider.GetRequiredService<NavigationStore>(),
                   () => new AccountVM(serviceProvider.GetRequiredService<AccountStore>(), SettingsNavigarionService(serviceProvider)),
                  () => CreateNavigationBarViewModel(serviceProvider));


            }
        }
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
//}


