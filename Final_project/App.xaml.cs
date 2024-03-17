using Final_project.Stores;
using Final_project.ViewModels;
using Microsoft.EntityFrameworkCore;
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
    public partial class App : System.Windows.Application
    {

        private readonly ReportModelDbContextFactory _reportDbContextFactory;

        private readonly IGetAllReportsQuery _query;
        private readonly ICreateReportCommand _createReportCommand;
        private readonly IDeleteReportCommand _deleteReportCommand;
        private readonly IUpdateReportCommand _updateReportCommand;


        private readonly SelectedReportStore _selectedReportStore;
        private readonly NavigationStore _navigationStore;
        private readonly ReportStore _reportStore;

        //this is for the report viewer the report instance 
        public static ReportStore ReportStoreInstance { get; private set; }

        public App()
        {



            Bold.Licensing.BoldLicenseProvider.RegisterLicense("rNO4kk3ljKWv4vBVFMMzZFi8W9yZY/VCNJZKWr/yqMk=");

            string connectionString = "Data Source =Reports.db";

            _reportDbContextFactory = new ReportModelDbContextFactory(
                new DbContextOptionsBuilder().UseSqlite(connectionString).Options);


            _query = new GetAllReportsQuery(_reportDbContextFactory);
            _createReportCommand = new CreateReportCommand(_reportDbContextFactory);
            _deleteReportCommand = new DeleteReportCommand(_reportDbContextFactory);
            _updateReportCommand = new UpdateReportCommand(_reportDbContextFactory);


            _reportStore = new ReportStore(_query, _createReportCommand, _deleteReportCommand, _updateReportCommand);


            _selectedReportStore = new SelectedReportStore(_reportStore);
            _navigationStore = new NavigationStore();


            // and this ,  
            ReportStoreInstance = new ReportStore(_query, _createReportCommand, _deleteReportCommand, _updateReportCommand);

        }

        protected override void OnStartup(StartupEventArgs e)
        {


            using (ReportModelDbContext context = _reportDbContextFactory.Create())
            {
                context.Database.Migrate();


            }



            HomeVM homeVM = new HomeVM(_reportStore, _selectedReportStore, _navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, homeVM)
            };

            MainWindow.Show();



            base.OnStartup(e);
        }

    }
}


