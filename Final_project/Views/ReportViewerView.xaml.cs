using BoldReports.Windows;
using Final_project.Other;
using Final_project.ViewModels;
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


                var x = new RowTablesCreatorForReportViewer();
                var viewModel = DataContext as ReportViewerVM;


                ComboBoxItem selectedReportItem = cmbReportSelection.SelectedItem as ComboBoxItem;


                string selectedReportPath = selectedReportItem.Tag.ToString();
                string fullReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Resources\", selectedReportPath);



                this.reportViewer.ReportPath = fullReportPath;
                this.reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                this.reportViewer.DataSources.Clear();



                // Handle the report data
                if (viewModel.SelectedReportData != null)
                {
                    DataTable reportDataTable = x.CreateReportDataTable(viewModel.SelectedReportData);
                    this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet1", Value = reportDataTable });


                    // Handle trykktesting data
                    if (viewModel.TrykktestingModels != null && viewModel.TrykktestingModels.Any())
                    {
                        DataTable tryktable = x.CreateTrykktestingTable(viewModel.TrykktestingModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet2", Value = tryktable });
                    }

                    if (viewModel.ReportImages != null && viewModel.ReportImages.Any())
                    {
                        int splitIndex = (viewModel.ReportImages.Count + 1) / 2; // Calculate the split point
                        DataTable imagesTable3 = x.CreateImagesDataTable(viewModel.ReportImages.Take(splitIndex).ToList());
                        DataTable imagesTable4 = x.CreateImagesDataTable2(viewModel.ReportImages.Skip(splitIndex).ToList());
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet3", Value = imagesTable3 });
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet4", Value = imagesTable4 });
                    }


                    // Handle ConcreteDensityModels data
                    if (viewModel.ConcreteDensityModels != null && viewModel.ConcreteDensityModels.Any())
                    {
                        DataTable concredensity = x.CreateConcreteDensityDataTable(viewModel.ConcreteDensityModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet5", Value = concredensity });
                    }


                    // Handle DataEtterKuttingOgSlipingModels data
                    if (viewModel.DataEtterKuttingOgSlipingModels != null && viewModel.DataEtterKuttingOgSlipingModels.Any())
                    {
                        DataTable dataetterkutting = x.DataEtterKuttingOgSlipingModelDataTable(viewModel.DataEtterKuttingOgSlipingModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet6", Value = dataetterkutting });
                    }


                    // Handle DataFraOppdragsgiverPrøverModels data
                    if (viewModel.DataFraOppdragsgiverPrøverModels != null && viewModel.DataFraOppdragsgiverPrøverModels.Any())
                    {
                        DataTable datafraoppdrag = x.DataFraOppdragsgiverPrøverModelDataTable(viewModel.DataFraOppdragsgiverPrøverModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet7", Value = datafraoppdrag });
                    }

                    this.reportViewer.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }


    }


}
