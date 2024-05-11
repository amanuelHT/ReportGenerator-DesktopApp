using Domain.Models;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace Final_project.Other
{
    public class RowTablesCreatorForReportViewer
    {
        public DataTable CreateReportDataTable(ReportModel reportData)
        {
            DataTable dataTable = new DataTable("SelectedReportData");
            if (reportData != null)
            {
                foreach (var property in reportData.GetType().GetProperties())
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }
                DataRow row = dataTable.NewRow();
                foreach (var property in reportData.GetType().GetProperties())
                {
                    row[property.Name] = property.GetValue(reportData) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }



        public DataTable CreateKontrollertAvTable(KontrollertAvførtAvModel kontrollertAvførtAvModels)
        {
            DataTable dataTable = new DataTable("kontrollertAvførtAvModels");
            if (kontrollertAvførtAvModels != null)
            {
                foreach (var property in kontrollertAvførtAvModels.GetType().GetProperties())
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }
                DataRow row = dataTable.NewRow();
                foreach (var property in kontrollertAvførtAvModels.GetType().GetProperties())
                {
                    row[property.Name] = property.GetValue(kontrollertAvførtAvModels) ?? DBNull.Value;

                }
                dataTable.Rows.Add(row);
            }
            return dataTable;


        }

        public DataTable CreateTestUtførtAvTable(TestUtførtAvModel testUtførtAvModels)
        {
            DataTable dataTable = new DataTable("TestUtførtAvModel");
            if (testUtførtAvModels != null)
            {
                foreach (var property in testUtførtAvModels.GetType().GetProperties())
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }
                DataRow row = dataTable.NewRow();
                foreach (var property in testUtførtAvModels.GetType().GetProperties())
                {
                    row[property.Name] = property.GetValue(testUtførtAvModels) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }




        //Image handling 
        public DataTable CreateImagesDataTable2(List<ReportImageModel> images)
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

        public DataTable CreateImagesDataTable(List<ReportImageModel> images)
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

        public byte[] GetImageData(string imagePath)
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

        public DataTable CreateTrykktestingTable(ObservableCollection<TrykktestingModel> trykktestingModels)
        {
            DataTable trykTable = new DataTable("TrykktestingData");
            trykTable.Columns.Add("Prøvenr", typeof(int));
            trykTable.Columns.Add("TrykkflateMm", typeof(decimal));
            trykTable.Columns.Add("PalastHastighetMPas", typeof(decimal));
            trykTable.Columns.Add("TrykkfasthetMPa", typeof(decimal));
            trykTable.Columns.Add("TrykkfasthetMPaNSE", typeof(decimal));

            foreach (var model in trykktestingModels)
            {
                DataRow row = trykTable.NewRow();
                row["Prøvenr"] = model.Prøvenr;
                row["TrykkflateMm"] = model.TrykkflateMm;
                row["PalastHastighetMPas"] = model.PalastHastighetMPas;
                row["TrykkfasthetMPa"] = model.TrykkfasthetMPa;
                row["TrykkfasthetMPaNSE"] = model.TrykkfasthetMPaNSE;
                trykTable.Rows.Add(row);
            }

            return trykTable;
        }


        public DataTable CreateConcreteDensityDataTable(ObservableCollection<ConcreteDensityModel> densities)
        {
            DataTable densityTable = new DataTable("ConcreteDensityData");
            densityTable.Columns.Add("Prøvenr", typeof(int));
            densityTable.Columns.Add("Dato", typeof(DateTime));
            densityTable.Columns.Add("MasseILuft", typeof(double));
            densityTable.Columns.Add("MasseIVannbad", typeof(double));
            densityTable.Columns.Add("Pw", typeof(double));
            densityTable.Columns.Add("V", typeof(double));
            densityTable.Columns.Add("Densitet", typeof(double));


            foreach (var density in densities)
            {
                DataRow row = densityTable.NewRow();
                row["Prøvenr"] = density.Prøvenr;
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


        public DataTable DataEtterKuttingOgSlipingModelDataTable(ObservableCollection<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels)
        {
            DataTable customTable = new DataTable("DataEtterKuttingOgSlipingModel");
            customTable.Columns.Add("Prøvenr", typeof(int));
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
                row["Prøvenr"] = model.Prøvenr;
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


        public DataTable DataFraOppdragsgiverPrøverModelDataTable(ObservableCollection<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels)
        {
            DataTable customTable = new DataTable("CustomData");
            customTable.Columns.Add("Prøvenr", typeof(int));
            customTable.Columns.Add("Datomottatt", typeof(DateTime));
            customTable.Columns.Add("Overdekningoppgitt", typeof(string));
            customTable.Columns.Add("Dmax", typeof(string));
            customTable.Columns.Add("KjerneImax", typeof(int));
            customTable.Columns.Add("KjerneImin", typeof(int));
            customTable.Columns.Add("OverflateOK", typeof(string));
            customTable.Columns.Add("OverflateUK", typeof(string));


            foreach (var model in dataFraOppdragsgiverPrøverModels)
            {
                DataRow row = customTable.NewRow();
                row["Prøvenr"] = model.Prøvenr;
                row["Datomottatt"] = model.Datomottatt;
                row["Overdekningoppgitt"] = model.Overdekningoppgitt;
                row["Dmax"] = model.Dmax;
                row["KjerneImax"] = model.KjerneImax;
                row["KjerneImin"] = model.KjerneImin;
                row["OverflateOK"] = model.OverflateOK;
                row["OverflateUK"] = model.OverflateUK;
                customTable.Rows.Add(row);
            }

            return customTable;
        }

        internal DataTable CreateTestModelsTable(ObservableCollection<TestModel> testModels)
        {
            DataTable customTable = new DataTable("CustomData");
            customTable.Columns.Add("Name", typeof(string));

            foreach (var model in testModels)
            {
                DataRow row = customTable.NewRow();
                row["Name"] = model.Name;

                customTable.Rows.Add(row);
            }

            return customTable;
        }

        internal DataTable CreateVerktøyModelsTable(ObservableCollection<verktøyModel> verktøyModels)
        {
            DataTable customTable = new DataTable("CustomData");
            customTable.Columns.Add("Name", typeof(string));

            foreach (var model in verktøyModels)
            {
                DataRow row = customTable.NewRow();
                row["Name"] = model.Name;

                customTable.Rows.Add(row);
            }

            return customTable;
        }
    }
}
