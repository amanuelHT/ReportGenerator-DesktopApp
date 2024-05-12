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


                if (ReportViewerViewModel.SelectedReportData == null)
                {

                    MessageBox.Show("Vennligst velg en rapport for å vise.", "Rapportvalg kreves", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }


                // raport data
                if (ReportViewerViewModel.SelectedReportData != null)
                {






                    DataTable reportDataTable = x.CreateReportDataTable(ReportViewerViewModel.SelectedReportData);
                    this.reportViewer.DataSources.Add(new ReportDataSource { Name = "ReportModels", Value = reportDataTable });


                    DataTable testutførtav = x.CreateTestUtførtAvTable(ReportViewerViewModel.TestUtførtAvModels);
                    this.reportViewer.DataSources.Add(new ReportDataSource { Name = "TestUtførtAvModel", Value = testutførtav });


                    DataTable kontrollertav = x.CreateKontrollertAvTable(ReportViewerViewModel.KontrollertAvførtAvModels);
                    this.reportViewer.DataSources.Add(new ReportDataSource { Name = "KontrollertAvførtAvModel", Value = kontrollertav });




                    if (ReportViewerViewModel.TestModels != null && ReportViewerViewModel.TestModels.Any())
                    {
                        DataTable tests = x.CreateTestModelsTable(ReportViewerViewModel.TestModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "TestModel", Value = tests });
                    }


                    if (ReportViewerViewModel.VerktøyModels != null && ReportViewerViewModel.VerktøyModels.Any())
                    {
                        DataTable verktøy = x.CreateVerktøyModelsTable(ReportViewerViewModel.VerktøyModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "verktøyModel", Value = verktøy });
                    }


                    if (ReportViewerViewModel.TrykktestingModels != null && ReportViewerViewModel.TrykktestingModels.Any())
                    {
                        DataTable tryktable = x.CreateTrykktestingTable(ReportViewerViewModel.TrykktestingModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "TrykktestingModel", Value = tryktable });
                    }

                    if (ReportViewerViewModel.ReportImages != null && ReportViewerViewModel.ReportImages.Any())
                    {
                        int splitIndex = (ReportViewerViewModel.ReportImages.Count + 1) / 2; // split point
                        DataTable imagesTable3 = x.CreateImagesDataTable(ReportViewerViewModel.ReportImages.Take(splitIndex).ToList());
                        DataTable imagesTable4 = x.CreateImagesDataTable2(ReportViewerViewModel.ReportImages.Skip(splitIndex).ToList());
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "ReportImageModel", Value = imagesTable3 });
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "ReportImageModel2", Value = imagesTable4 });
                    }

                    if (ReportViewerViewModel.ConcreteDensityModels != null && ReportViewerViewModel.ConcreteDensityModels.Any())
                    {
                        DataTable concredensity = x.CreateConcreteDensityDataTable(ReportViewerViewModel.ConcreteDensityModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "ConcreteDensityModel", Value = concredensity });
                    }

                    if (ReportViewerViewModel.DataEtterKuttingOgSlipingModels != null && ReportViewerViewModel.DataEtterKuttingOgSlipingModels.Any())
                    {
                        DataTable dataetterkutting = x.DataEtterKuttingOgSlipingModelDataTable(ReportViewerViewModel.DataEtterKuttingOgSlipingModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataEtterKuttingOgSlipingModel", Value = dataetterkutting });
                    }


                    if (ReportViewerViewModel.DataFraOppdragsgiverPrøverModels != null && ReportViewerViewModel.DataFraOppdragsgiverPrøverModels.Any())
                    {
                        DataTable datafraoppdrag = x.DataFraOppdragsgiverPrøverModelDataTable(ReportViewerViewModel.DataFraOppdragsgiverPrøverModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataFraOppdragsgiverPrøverModel", Value = datafraoppdrag });
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
