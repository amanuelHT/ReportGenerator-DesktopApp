using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Service;
using Final_project.Stores;
using Final_project.ViewModels;
using Final_project.Views;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Report_Generator_Domain.Commands;
using Report_Generator_Domain.ITables;
using Report_Generator_Domain.Queries;
using Report_Generator_EntityFramework;
using Report_Generator_EntityFramework.Commands;
using Report_Generator_EntityFramework.Queries;
using Report_Generator_EntityFramework.Tables;
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

                 string firebaseApiKey = context.Configuration.GetValue<string>("FIREBASE_API_KEY");
                 FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
                 service.AddSingleton<FirebaseAuthProvider>(firebaseAuthProvider);



                 //Configures and registers a SQLite database connection for ReportModelDbContext using dependency injection.
                 string? connectionString = context.Configuration.GetConnectionString("Sqlite");
                 service.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                 service.AddSingleton<ReportModelDbContextFactory>();
                 Stores.FirestoreHelper.InitializeFirestore();

                 service.AddSingleton<Func<Type, ObservableObject>>(serviceProvider => type =>
                 (ObservableObject)serviceProvider.GetRequiredService(type));


                 //navigation service 
                 service.AddSingleton<INavigation, Navigation>();


                 //                //Queries and commands for database services
                 service.AddSingleton<IGetAllReportsQuery, GetAllReportsQuery>();
                 service.AddSingleton<ICreateReportCommand, CreateReportCommand>();
                 service.AddSingleton<IDeleteReportCommand, DeleteReportCommand>();
                 service.AddSingleton<IUpdateReportCommand, UpdateReportCommand>();
                 service.AddSingleton<IGetReportDataCommand, GetReportDataCommand>();
                 service.AddSingleton<ICreateImageCommand, ImageCreationCommand>();


                 service.AddSingleton<IDeleteReportImageCommand, ImageDeletionCommand>();


                 service.AddSingleton<ICreateDataFraOppdragsgiverPrøverModelCommand, CreateDataFraOppdragsgiverPrøverModelCommand>();




                 //                //stores , Single source of truth, defnitly Singlton
                 service.AddSingleton<SelectedReportStore>();
                 service.AddSingleton<ModalNavigation>();
                 service.AddSingleton<AccountStore>();
                 service.AddSingleton<NavigationStore>();
                 service.AddSingleton<GeneratedReportStore>();
                 service.AddSingleton<ReportStore>();
                 service.AddSingleton<ModalWindow>();



                 service.AddSingleton<ImageCollectionVM>();
                 service.AddSingleton<ImageVM>();
                 service.AddSingleton<MainViewModel>();
                 service.AddSingleton<INavigationService>(s => LoginNavigarionService(s));

                 //the reasion we make them transient is we dispose our viewmodel,
                 //if we dispose that means we are not going to use it again, we are going to resolve a new instance
                 //if singlton we are going to get a new instance every time venen though disposed

                 service.AddTransient<RoleManagementVM>(provider =>
                       new RoleManagementVM(provider.GetRequiredService<FirebaseAuthProvider>()));

                 service.AddTransient<ResetPasswordVM>(provider =>
                       new ResetPasswordVM(provider.GetRequiredService<FirebaseAuthProvider>()));


                 service.AddTransient<HomeVM>(s =>
                       new HomeVM(
                             s.GetRequiredService<ModalWindow>(),
                               s.GetRequiredService<ModalNavigation>(),
                               s.GetRequiredService<ReportStore>(),
                               s.GetRequiredService<SelectedReportStore>(),
                               s.GetRequiredService<NavigationStore>(),
                                  s.GetRequiredService<INavigationService>())
                               );


                 service.AddTransient<SettingsVM>(s => new SettingsVM(LoginNavigarionService(s)));


                 //service.AddTransient<UserInfoVM>();


                 service.AddTransient<UserInfoVM>(s => new UserInfoVM(firebaseAuthProvider, RoleManagementNavigationService(s)));

                 service.AddTransient<AccountVM>(s =>
                        new AccountVM(s.GetRequiredService<AccountStore>(),
                        SettingsNavigarionService(s)));



                 //service.AddTransient<LoginVM>(s => new LoginVM(ResetPasswordNavigarionService(s)));

                 service.AddTransient<LoginVM>(s => new LoginVM(
                       s.GetRequiredService<AccountStore>(),
                       AccountNavigarionService(s),
                       s.GetRequiredService<FirebaseAuthProvider>(),
                       ResetPasswordNavigarionService(s)));
                 // Notice how each service is retrieved inside the lambda

                 //service.AddTransient<LoginVM>(s =>
                 //       new LoginVM(s.GetRequiredService<AccountStore>(),
                 //       AccountNavigarionService(s),
                 //       s.GetRequiredKeyedService<firebaseAuthProvider>()));

                 service.AddTransient<GeneratedReportListVM>(s =>
                  new GeneratedReportListVM(s.GetRequiredService<GeneratedReportStore>(),
                     GeneratedRListNavigationService(s)));

                 service.AddTransient<ReportViewerVM>(s =>
                        new ReportViewerVM(s.GetRequiredService<ReportStore>(),
                      GeneratedRListNavigationService(s)));









                 service.AddSingleton<NavigationBarVM>(CreateNavigationBarViewModel);

                 service.AddSingleton<MainWindow>((services) => new MainWindow()
                 {
                     DataContext = services.GetRequiredService<MainViewModel>()
                 });


                 Bold.Licensing.BoldLicenseProvider.RegisterLicense
                 (
                      context.Configuration.GetValue<string>("Licensekey")
                  );


             })
               .Build();
            _serviceProvider = _host.Services;

        }



        protected override void OnStartup(StartupEventArgs e)
        {


            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();


            //ReportModelDbContextFactory reportModelDbContextFactory =
            //     _host.Services.GetRequiredService<ReportModelDbContextFactory>();
            //using (
            //    ReportModelDbContext context = reportModelDbContextFactory.Create())

            //{
            //    context.Database.Migrate();

            //}

            // Set up the main window
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);

        }


        private NavigationBarVM CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {

            return new NavigationBarVM(
                 serviceProvider.GetRequiredService<AccountStore>(),
                  LoginNavigarionService(serviceProvider),
                  SettingsNavigarionService(serviceProvider),
                    AccountNavigarionService(serviceProvider),
                       GeneratedRListNavigationService(serviceProvider),
                       ReportViewerNavigationService(serviceProvider),
                       HomeNavigationService(serviceProvider),
                       RoleManagementNavigationService(serviceProvider),
                       UserInfoNavigationService(serviceProvider)
                       );
        }

        private INavigationService RoleManagementNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<RoleManagementVM>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<RoleManagementVM>(),
                () => serviceProvider.GetRequiredService<NavigationBarVM>());
        }


        private INavigationService SettingsNavigarionService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<SettingsVM>(
               serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<SettingsVM>(),
                 () => serviceProvider.GetRequiredService<NavigationBarVM>());
        }

        private INavigationService HomeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeVM>(
                  serviceProvider.GetRequiredService<NavigationStore>(),
                   () => serviceProvider.GetRequiredService<HomeVM>(),
                  () => serviceProvider.GetRequiredService<NavigationBarVM>());

        }

        private INavigationService UserInfoNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<UserInfoVM>(
                  serviceProvider.GetRequiredService<NavigationStore>(),
                   () => serviceProvider.GetRequiredService<UserInfoVM>(),
                  () => serviceProvider.GetRequiredService<NavigationBarVM>());

        }

        private INavigationService GeneratedRListNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<GeneratedReportListVM>(
                  serviceProvider.GetRequiredService<NavigationStore>(),
                   () => serviceProvider.GetRequiredService<GeneratedReportListVM>(),
                  () => serviceProvider.GetRequiredService<NavigationBarVM>());

        }
        private INavigationService ReportViewerNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ReportViewerVM>(
                  serviceProvider.GetRequiredService<NavigationStore>(),
                   () => serviceProvider.GetRequiredService<ReportViewerVM>(),
                  () => serviceProvider.GetRequiredService<NavigationBarVM>());

        }
        private INavigationService ResetPasswordNavigarionService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ResetPasswordVM>(
                  serviceProvider.GetRequiredService<NavigationStore>(),
                   () => serviceProvider.GetRequiredService<ResetPasswordVM>(),
                  () => serviceProvider.GetRequiredService<NavigationBarVM>());

        }

        private INavigationService LoginNavigarionService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<LoginVM>(
              serviceProvider.GetRequiredService<NavigationStore>(),
                 () => new LoginVM(
                serviceProvider.GetRequiredService<AccountStore>(),
                AccountNavigarionService(serviceProvider),
                serviceProvider.GetRequiredService<FirebaseAuthProvider>(),
                ResetPasswordNavigarionService(serviceProvider)
            ),
            () => CreateNavigationBarViewModel(serviceProvider)
        );
        }


        //private INavigationService LoginNavigarionService(IServiceProvider serviceProvider)
        //{
        //    return new LayoutNavigationService<LoginVM>(
        //          serviceProvider.GetRequiredService<NavigationStore>(),
        //           () => new LoginVM(serviceProvider.GetRequiredService<AccountStore>(), 
        //           serviceProvider.GetRequiredService<FirebaseAuthProvider>(), 
        //           AccountNavigarionService(serviceProvider)),
        //           () => CreateNavigationBarViewModel(serviceProvider));

        //}

        private INavigationService AccountNavigarionService(IServiceProvider serviceProvider)
        {
            {
                return new LayoutNavigationService<AccountVM>(
                  serviceProvider.GetRequiredService<NavigationStore>(),
                   () => serviceProvider.GetRequiredService<AccountVM>(),
                  () => serviceProvider.GetRequiredService<NavigationBarVM>());
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


