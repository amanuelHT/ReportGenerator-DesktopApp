using Final_project.Stores;
using Final_project.ViewModels;
using Final_project.Views;
using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IServiceProvider ServiceProvider { get; private set; }

        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, service) =>
                {

                    // Configures and registers a SQLite database connection for ReportModelDbContext using dependency injection.
                    string? connectionString = context.Configuration.GetConnectionString("Sqlite");
                    service.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                    service.AddSingleton<ReportModelDbContextFactory>();





                    //Queries and commands for database services
                    service.AddSingleton<IGetAllReportsQuery, GetAllReportsQuery>();
                    service.AddSingleton<ICreateReportCommand, CreateReportCommand>();
                    service.AddSingleton<IDeleteReportCommand, DeleteReportCommand>();
                    service.AddSingleton<IUpdateReportCommand, UpdateReportCommand>();


                    //stores , Single source of truth, defnitly Singlton
                    service.AddSingleton<ReportStore>();
                    service.AddSingleton<SelectedReportStore>();
                    service.AddSingleton<NavigationStore>();


                    //viewmodels 

                    service.AddSingleton<MainViewModel>();
                    service.AddTransient<HomeVM>(CreateHomeViewModel);
                    service.AddSingleton<ReportViewerVM>();
                    service.AddSingleton<ReportViewerView>();


                    service.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });



                })
                .Build();



            Bold.Licensing.BoldLicenseProvider.RegisterLicense("rNO4kk3ljKWv4vBVFMMzZFi8W9yZY/VCNJZKWr/yqMk=");


        }

        private HomeVM CreateHomeViewModel(IServiceProvider service)
        {
            return HomeVM.LoadHome(
                 service.GetRequiredService<ReportStore>(),
                 service.GetRequiredService<SelectedReportStore>(),
                 service.GetRequiredService<NavigationStore>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            ServiceProvider = _host.Services;


            ReportModelDbContextFactory reportModelDbContextFactory =
                 _host.Services.GetRequiredService<ReportModelDbContextFactory>();
            using (ReportModelDbContext context = reportModelDbContextFactory.Create())

            {
                context.Database.Migrate();

            }


            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            // MainWindow.Show();



            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();
        }
    }
}


