using Final_project.ViewModels;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace Final_project.Views
{
    public partial class ReportViewerView : UserControl
    {
        public ReportViewerView()
        {
            InitializeComponent();
            this.Loaded += OnGenerateReportClick;
        }

        private async void OnGenerateReportClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var viewModel = DataContext as ReportViewerVM;

                this.reportViewer.ReportPath = Path.Combine(Environment.CurrentDirectory, @"Resources\Report1.rdlc");
                this.reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                this.reportViewer.DataSources.Clear();

                // Use the selected report's data
                if (viewModel.SelectedReportData != null)
                {
                    DataTable reportDataTable = new DataTable("SelectedReportData");

                    // Add columns for each property
                    foreach (var property in viewModel.SelectedReportData.GetType().GetProperties())
                    {
                        reportDataTable.Columns.Add(property.Name, property.PropertyType);
                    }

                    // Add a row with values of those properties
                    DataRow row = reportDataTable.NewRow();
                    foreach (var property in viewModel.SelectedReportData.GetType().GetProperties())
                    {
                        row[property.Name] = property.GetValue(viewModel.SelectedReportData);
                    }
                    reportDataTable.Rows.Add(row);

                    // Add images to the dataset
                    // Add images to the dataset
                    if (viewModel.ReportImages != null && viewModel.ReportImages.Any())
                    {
                        DataTable imageDataTable = new DataTable("ReportImages");
                        imageDataTable.Columns.Add("Image", typeof(byte[]));

                        foreach (var imageModel in viewModel.ReportImages)
                        {
                            byte[] imageData = await GetImageData(imageModel.ImageUrl);
                            DataRow imageRow = imageDataTable.NewRow();
                            imageRow["Image"] = imageData;
                            imageDataTable.Rows.Add(imageRow);
                        }

                        this.reportViewer.DataSources.Add(new BoldReports.Windows.ReportDataSource { Name = "DataSet2", Value = imageDataTable });
                    }



                    this.reportViewer.DataSources.Add(new BoldReports.Windows.ReportDataSource { Name = "DataSet1", Value = reportDataTable });
                    this.reportViewer.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }

        private async Task<byte[]> GetImageData(string imageUrl)
        {
            if (imageUrl.StartsWith("http://") || imageUrl.StartsWith("https://"))
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(imageUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        throw new Exception($"Failed to download image from {imageUrl}");
                    }
                }
            }
            else if (imageUrl.StartsWith("file://"))
            {
                // Convert file URI to a local path
                string localPath = new Uri(imageUrl).LocalPath;
                return File.ReadAllBytes(localPath);
            }
            else
            {
                throw new Exception($"Unsupported image URL scheme in {imageUrl}");
            }


        }

    }
}
