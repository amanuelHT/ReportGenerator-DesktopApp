using Final_project.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Final_project.Views
{
    public partial class ReportViewerView : UserControl
    {

        public ReportViewerView()
        {
            InitializeComponent();
            // Access the ServiceProvider from the App class and use it to get the required service

            //var reportStore = ((App)Application.Current).ServiceProvider.GetRequiredService<ReportStore>();
            // DataContext = new ReportViewerVM(reportStore);

            this.Loaded += OnGenerateReportClick;

        }




        private void OnGenerateReportClick(object sender, RoutedEventArgs e)
        {
            try
            {

                var viewModel = DataContext as ReportViewerVM;

                // Assuming "ProductList.GetData()" is your data source method
                this.reportViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Resources\Report1.rdlc");
                this.reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                this.reportViewer.DataSources.Clear();
                this.reportViewer.DataSources.Add(new BoldReports.Windows.ReportDataSource { Name = "DataSet1", Value = viewModel.Reports });
                this.reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }
    }
}
