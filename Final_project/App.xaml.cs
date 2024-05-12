using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Components;
using Final_project.Service;
using Final_project.Stores;
using Final_project.ViewModels;
using Final_project.ViewModels.ReportComponentsVM;
using Final_project.Views;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Report_Generator_Domain.Commands;

using Report_Generator_Domain.Queries;
using Report_Generator_EntityFramework;
using Report_Generator_EntityFramework.Commands;
using Report_Generator_EntityFramework.Queries;
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



                 //registeration a SQLite database connection for ReportModelDbContext
                 string? connectionString = context.Configuration.GetConnectionString("Sqlite");
                 service.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                 service.AddSingleton<ReportModelDbContextFactory>();
                 Stores.FirestoreHelper.InitializeFirestoreAndStorage();

                 service.AddSingleton<Func<Type, ObservableObject>>(serviceProvider => type =>
                 (ObservableObject)serviceProvider.GetRequiredService(type));


                 service.AddSingleton<INavigation, Navigation>();


                 service.AddSingleton<IGetAllReportsQuery, GetAllReportsQuery>();
                 service.AddSingleton<ICreateReportCommand, CreateReportCommand>();
                 service.AddSingleton<IDeleteReportCommand, Report_Generator_EntityFramework.Commands.DeleteReportCommand>();
                 service.AddSingleton<IUpdateReportCommand, UpdateReportCommand>();

                 service.AddSingleton<IGetReportQuery, GetReportQuery>();




                 service.AddSingleton<SelectedReportStore>();
                 service.AddSingleton<ModalNavigation>();
                 service.AddSingleton<AccountStore>();
                 service.AddSingleton<NavigationStore>();
                 service.AddSingleton<FirebaseStore>();

                 service.AddSingleton<GeneratedReportStore>();
                 service.AddSingleton<ReportStore>();

                 service.AddTransient<ModalWindow>();
                 service.AddTransient<MessageView>();



                 service.AddSingleton<ImageCollectionVM>();
                 service.AddSingleton<ImageVM>();
                 service.AddSingleton<MainViewModel>();
                 service.AddSingleton<KundeServiceVM>();
                 service.AddSingleton<MessageVM>();
                 service.AddSingleton<TestVM>();
                 service.AddSingleton<TestCollectionVM>();

                 // initial navigation 
                 service.AddSingleton<INavigationService>(s => HomeNavigationService(s));

                 //the reasion we make them transient is we dispose our viewmodel,
                 //if we dispose that means we are not going to use it again,
                 //we are going to resolve a new instance
                 //if singlton we are going to get a new instance every time venen though disposed

                 service.AddTransient<RoleManagementVM>(s =>
                       new RoleManagementVM(s.GetRequiredService<FirebaseAuthProvider>(),
                       UserInfoNavigationService(s)));

                 service.AddTransient<ResetPasswordVM>(s =>
                       new ResetPasswordVM(s.GetRequiredService<FirebaseAuthProvider>(),
                       s.GetRequiredService<INavigationService>()));


                 service.AddTransient<HomeVM>(s =>
                       new HomeVM(
                             s.GetRequiredService<ModalWindow>(),
                               s.GetRequiredService<ModalNavigation>(),
                               s.GetRequiredService<ReportStore>(),
                               s.GetRequiredService<SelectedReportStore>(),
                               s.GetRequiredService<NavigationStore>(),
                                  s.GetRequiredService<INavigationService>())
                               );


                 service.AddTransient<SettingsVM>(s => new SettingsVM(SettingsNavigarionService(s)));


                 service.AddTransient<UserInfoVM>(s => new UserInfoVM(
                     firebaseAuthProvider,
                     RoleManagementNavigationService(s),
                     s.GetRequiredService<FirebaseStore>()
                 ));

                 service.AddTransient<AccountVM>(s =>
                        new AccountVM(s.GetRequiredService<AccountStore>(),
                        SettingsNavigarionService(s)));



                 service.AddTransient<LoginVM>(s => new LoginVM(
                       s.GetRequiredService<AccountStore>(),
                       AccountNavigarionService(s),
                       s.GetRequiredService<FirebaseAuthProvider>(),
                       ResetPasswordNavigarionService(s),
                       HomeNavigationService(s)));
                 service.AddSingleton(s => new LogoutCommand(
                       s.GetRequiredService<AccountStore>(),
                       HomeNavigationService(s)));

                 service.AddTransient<GeneratedReportListVM>(s =>
                  new GeneratedReportListVM(
                      s.GetRequiredService<FirebaseStore>()));

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
                      LoginNavigarionService(serviceProvider),
                       GeneratedRListNavigationService(serviceProvider),
                       ReportViewerNavigationService(serviceProvider),
                       HomeNavigationService(serviceProvider),
                       RoleManagementNavigationService(serviceProvider),
                       UserInfoNavigationService(serviceProvider),
                      KundeServiceNavigationService(serviceProvider));


        }

        private INavigationService RoleManagementNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<RoleManagementVM>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<RoleManagementVM>(),
                () => serviceProvider.GetRequiredService<NavigationBarVM>());
        }

        private INavigationService KundeServiceNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<KundeServiceVM>(
                           serviceProvider.GetRequiredService<NavigationStore>(),
                   () => serviceProvider.GetRequiredService<KundeServiceVM>(),
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
                   () => serviceProvider.GetRequiredService<LoginVM>(),
                  () => serviceProvider.GetRequiredService<NavigationBarVM>());
        }



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

