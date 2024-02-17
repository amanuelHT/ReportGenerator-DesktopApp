using Final_project.Stores;
using Final_project.ViewModels;
using System.Windows;

namespace Final_project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        private readonly SelectedReportStore _selectedReportStore;
        private readonly NavigationStore _navigationStore;
        private readonly ReportStore _reportStore;


        public App()
        {
            _reportStore = new ReportStore();
            _selectedReportStore = new SelectedReportStore(_reportStore);
            _navigationStore = new NavigationStore();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
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


