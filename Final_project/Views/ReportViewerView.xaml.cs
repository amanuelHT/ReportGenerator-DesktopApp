using System.Windows;

namespace Final_project.Views
{
    public partial class ReportViewerView : System.Windows.Controls.UserControl
    {
        public ReportViewerView()
        {
            InitializeComponent();

            this.Loaded += ReportViewerView_Loaded;
        }


        public void ReportViewerView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Resources\product-list.rdlc");
                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show("Report file not found.");
                    return;
                }

                this.reportViewer.ReportPath = reportPath;
                this.reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                this.reportViewer.DataSources.Clear();

                var reportData = ProductList.GetData();
                if (reportData == null || reportData.Count == 0)
                {
                    MessageBox.Show("No data available to display in the report.");
                    return;
                }

                this.reportViewer.DataSources.Add(new BoldReports.Windows.ReportDataSource { Name = "list", Value = reportData });
                this.reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}");
            }

        }
    }
}