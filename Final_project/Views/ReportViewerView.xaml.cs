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




                var ReportViewerViewModel = DataContext as ReportViewerVM;

                this.reportViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Resources\Report1.rdlc");
                this.reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
                this.reportViewer.DataSources.Clear();


                // Handle the report data
                if (ReportViewerViewModel.SelectedReportData != null)
                {
                    DataTable reportDataTable = x.CreateReportDataTable(ReportViewerViewModel.SelectedReportData);
                    this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet1", Value = reportDataTable });


                    // Handle trykktesting data
                    if (ReportViewerViewModel.TrykktestingModels != null && ReportViewerViewModel.TrykktestingModels.Any())
                    {
                        DataTable tryktable = x.CreateTrykktestingTable(ReportViewerViewModel.TrykktestingModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet2", Value = tryktable });
                    }

                    // Handle images data
                    if (ReportViewerViewModel.ReportImages != null && ReportViewerViewModel.ReportImages.Any())
                    {
                        int splitIndex = (ReportViewerViewModel.ReportImages.Count + 1) / 2; // Calculate the split point
                        DataTable imagesTable3 = x.CreateImagesDataTable(ReportViewerViewModel.ReportImages.Take(splitIndex).ToList());
                        DataTable imagesTable4 = x.CreateImagesDataTable2(ReportViewerViewModel.ReportImages.Skip(splitIndex).ToList());
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet3", Value = imagesTable3 });
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet4", Value = imagesTable4 });
                    }

                    // Handle ConcreteDensityModels data
                    if (ReportViewerViewModel.ConcreteDensityModels != null && ReportViewerViewModel.ConcreteDensityModels.Any())
                    {
                        DataTable concredensity = x.CreateConcreteDensityDataTable(ReportViewerViewModel.ConcreteDensityModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet5", Value = concredensity });
                    }

                    // Handle DataEtterKuttingOgSlipingModels data
                    if (ReportViewerViewModel.DataEtterKuttingOgSlipingModels != null && ReportViewerViewModel.DataEtterKuttingOgSlipingModels.Any())
                    {
                        DataTable dataetterkutting = x.DataEtterKuttingOgSlipingModelDataTable(ReportViewerViewModel.DataEtterKuttingOgSlipingModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet6", Value = dataetterkutting });
                    }


                    // Handle DataFraOppdragsgiverPrøverModels data
                    if (ReportViewerViewModel.DataFraOppdragsgiverPrøverModels != null && ReportViewerViewModel.DataFraOppdragsgiverPrøverModels.Any())
                    {
                        DataTable datafraoppdrag = x.DataFraOppdragsgiverPrøverModelDataTable(ReportViewerViewModel.DataFraOppdragsgiverPrøverModels);
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
