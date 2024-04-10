using BoldReports.Windows;
using Domain.Models;
using Final_project.ViewModels;
using System.Collections.ObjectModel;
using System.Data;
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

        private void OnGenerateReportClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var viewModel = DataContext as ReportViewerVM;

                this.reportViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Resources\Report1.rdlc");
                this.reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                this.reportViewer.DataSources.Clear();

                // Handle the report data
                if (viewModel.SelectedReportData != null)
                {
                    DataTable reportDataTable = CreateReportDataTable(viewModel.SelectedReportData);
                    this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet1", Value = reportDataTable });

                    // Handle the images
                    if (viewModel.ReportImages != null && viewModel.ReportImages.Any())
                    {
                        DataTable imagesTable = CreateImagesDataTable(viewModel.ReportImages);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet3", Value = imagesTable });
                    }

                    this.reportViewer.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }

        private DataTable CreateReportDataTable(ReportModel reportData)
        {
            DataTable dataTable = new DataTable("SelectedReportData");

            // Reflectively add columns for each property
            foreach (var property in reportData.GetType().GetProperties())
            {
                dataTable.Columns.Add(property.Name, property.PropertyType);
            }

            // Add a row with values of those properties
            DataRow row = dataTable.NewRow();
            foreach (var property in reportData.GetType().GetProperties())
            {
                row[property.Name] = property.GetValue(reportData);
            }
            dataTable.Rows.Add(row);

            return dataTable;
        }

        private DataTable CreateImagesDataTable(ObservableCollection<ReportImageModel> images)
        {
            DataTable imagesTable = new DataTable("ReportImages");
            for (int i = 1; i <= 3; i++)
            {
                imagesTable.Columns.Add("Image" + i, typeof(byte[]));
            }

            DataRow newRow = imagesTable.NewRow();
            for (int i = 0; i < images.Count && i < 3; i++)
            {
                newRow["Image" + (i + 1)] = GetImageData(images[i].ImageUrl);
            }
            imagesTable.Rows.Add(newRow);

            return imagesTable;
        }



        private byte[] GetImageData(string imageUrl)
        {
            try
            {
                // Create a Uri from imageUrl
                var uri = new Uri(imageUrl);

                // Convert the Uri to a local path
                string localPath = uri.LocalPath;

                // Read the file as a byte array
                return System.IO.File.ReadAllBytes(localPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load image data from URL: {imageUrl}", ex);
            }
        }



    }
}
