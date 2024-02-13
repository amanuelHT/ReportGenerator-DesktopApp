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


        public App()
        {
            _selectedReportStore = new SelectedReportStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new HomeVM(_selectedReportStore)
            };

            MainWindow.Show();



            base.OnStartup(e);
        }

    }
}


