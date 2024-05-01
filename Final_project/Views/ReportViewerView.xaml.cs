using BoldReports.Windows;
using Domain.Models;
using Final_project.ViewModels;
using Report_Generator_Domain.Models;
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

                    // Handle trykktesting data
                    if (viewModel.TrykktestingModels != null && viewModel.TrykktestingModels.Any())
                    {
                        DataTable tryktable = CreateTrykktestingTable(viewModel.TrykktestingModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet2", Value = tryktable });
                    }

                    if (viewModel.ReportImages != null && viewModel.ReportImages.Any())
                    {
                        int splitIndex = (viewModel.ReportImages.Count + 1) / 2; // Calculate the split point
                        DataTable imagesTable3 = CreateImagesDataTable(viewModel.ReportImages.Take(splitIndex).ToList());
                        DataTable imagesTable4 = CreateImagesDataTable2(viewModel.ReportImages.Skip(splitIndex).ToList());
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet3", Value = imagesTable3 });
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet4", Value = imagesTable4 });
                    }


                    // Handle ConcreteDensityModels data
                    if (viewModel.ConcreteDensityModels != null && viewModel.ConcreteDensityModels.Any())
                    {
                        DataTable concredensity = CreateConcreteDensityDataTable(viewModel.ConcreteDensityModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet5", Value = concredensity });
                    }


                    // Handle DataEtterKuttingOgSlipingModels data
                    if (viewModel.DataEtterKuttingOgSlipingModels != null && viewModel.DataEtterKuttingOgSlipingModels.Any())
                    {
                        DataTable dataetterkutting = DataEtterKuttingOgSlipingModelDataTable(viewModel.DataEtterKuttingOgSlipingModels);
                        this.reportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet6", Value = dataetterkutting });
                    }






                    // Handle the images
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
            foreach (var property in reportData.GetType().GetProperties())
            {
                dataTable.Columns.Add(property.Name, property.PropertyType);
            }
            DataRow row = dataTable.NewRow();
            foreach (var property in reportData.GetType().GetProperties())
            {
                row[property.Name] = property.GetValue(reportData);
            }
            dataTable.Rows.Add(row);
            return dataTable;
        }

        //Image handling 
        private DataTable CreateImagesDataTable2(List<ReportImageModel> images)
        {
            DataTable imagesTable = new DataTable("ReportImages2");
            imagesTable.Columns.Add("Name2", typeof(string));
            imagesTable.Columns.Add("Image2", typeof(byte[]));

            foreach (var image in images)
            {
                DataRow row = imagesTable.NewRow();
                row["Name2"] = image.Name;
                row["Image2"] = GetImageData(image.ImageUrl);
                imagesTable.Rows.Add(row);
            }

            return imagesTable;
        }

        private DataTable CreateImagesDataTable(List<ReportImageModel> images)
        {
            DataTable imagesTable = new DataTable("ReportImages");
            imagesTable.Columns.Add("Name", typeof(string));
            imagesTable.Columns.Add("Image", typeof(byte[]));

            foreach (var image in images)
            {
                DataRow row = imagesTable.NewRow();
                row["Name"] = image.Name;
                row["Image"] = GetImageData(image.ImageUrl);
                imagesTable.Rows.Add(row);
            }

            return imagesTable;
        }

        private byte[] GetImageData(string imagePath)
        {
            try
            {
                if (Uri.TryCreate(imagePath, UriKind.RelativeOrAbsolute, out Uri uri))
                {
                    if (uri.IsFile)
                    {
                        string localPath = uri.LocalPath;
                        return System.IO.File.ReadAllBytes(localPath);
                    }
                    else
                    {
                        throw new InvalidOperationException("Web URLs are not supported directly.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid image path or URL.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load image data: {ex.Message}");
                return null;
            }
        }


        //tables 

        private DataTable CreateTrykktestingTable(ObservableCollection<TrykktestingModel> trykktestingModels)
        {
            DataTable trykTable = new DataTable("TrykktestingData");
            trykTable.Columns.Add("TrykkflateMm", typeof(decimal));
            trykTable.Columns.Add("PalastHastighetMPas", typeof(decimal));
            trykTable.Columns.Add("TrykkfasthetMPa", typeof(decimal));
            trykTable.Columns.Add("TrykkfasthetMPaNSE", typeof(decimal));

            foreach (var model in trykktestingModels)
            {
                DataRow row = trykTable.NewRow();
                row["TrykkflateMm"] = model.TrykkflateMm;
                row["PalastHastighetMPas"] = model.PalastHastighetMPas;
                row["TrykkfasthetMPa"] = model.TrykkfasthetMPa;
                row["TrykkfasthetMPaNSE"] = model.TrykkfasthetMPaNSE;
                trykTable.Rows.Add(row);
            }

            return trykTable;
        }


        private DataTable CreateConcreteDensityDataTable(ObservableCollection<ConcreteDensityModel> densities)
        {
            DataTable densityTable = new DataTable("ConcreteDensityData");
            densityTable.Columns.Add("Id", typeof(int));
            densityTable.Columns.Add("Dato", typeof(DateTime));
            densityTable.Columns.Add("MasseILuft", typeof(double));
            densityTable.Columns.Add("MasseIVannbad", typeof(double));
            densityTable.Columns.Add("Pw", typeof(double));
            densityTable.Columns.Add("V", typeof(double));
            densityTable.Columns.Add("Densitet", typeof(double));


            foreach (var density in densities)
            {
                DataRow row = densityTable.NewRow();
                row["Id"] = density.Id;
                row["Dato"] = density.Dato;
                row["MasseILuft"] = density.MasseILuft;
                row["MasseIVannbad"] = density.MasseIVannbad;
                row["Pw"] = density.Pw;
                row["V"] = density.V;
                row["Densitet"] = density.Densitet;

                densityTable.Rows.Add(row);
            }

            return densityTable;
        }


        private DataTable DataEtterKuttingOgSlipingModelDataTable(ObservableCollection<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels)
        {
            DataTable customTable = new DataTable("CustomData");
            customTable.Columns.Add("IvannbadDato", typeof(DateTime));
            customTable.Columns.Add("TestDato", typeof(DateTime));
            customTable.Columns.Add("Overflatetilstand", typeof(string));
            customTable.Columns.Add("Dm", typeof(double));
            customTable.Columns.Add("Prøvetykke", typeof(double));
            customTable.Columns.Add("DmPrøvetykkeRatio", typeof(double));
            customTable.Columns.Add("TrykkfasthetMPa", typeof(double));
            customTable.Columns.Add("FasthetSammenligning", typeof(string));
            customTable.Columns.Add("FørSliping", typeof(double));
            customTable.Columns.Add("EtterSliping", typeof(double));
            customTable.Columns.Add("MmTilTopp", typeof(double));

            foreach (var model in dataEtterKuttingOgSlipingModels)
            {
                DataRow row = customTable.NewRow();
                row["IvannbadDato"] = model.IvannbadDato;
                row["TestDato"] = model.TestDato;
                row["Overflatetilstand"] = model.Overflatetilstand;
                row["Dm"] = model.Dm;
                row["Prøvetykke"] = model.Prøvetykke;
                row["DmPrøvetykkeRatio"] = model.DmPrøvetykkeRatio;
                row["TrykkfasthetMPa"] = model.TrykkfasthetMPa;
                row["FasthetSammenligning"] = model.FasthetSammenligning;
                row["FørSliping"] = model.FørSliping;
                row["EtterSliping"] = model.EtterSliping;
                row["MmTilTopp"] = model.MmTilTopp;
                customTable.Rows.Add(row);
            }

            return customTable;
        }


    }


}
