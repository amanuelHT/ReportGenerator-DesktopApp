using Domain.Models;
using Final_project.ViewModels;
using System.Windows;

namespace Final_project.Other
{
    internal class ReportValidator
    {
        // Validates the report model and its associated form
        public static bool IsValidReportModel(ReportModel reportModel, ReportFormVM reportForm)
        {
            List<string> validationErrors = new List<string>();

            if (string.IsNullOrEmpty(reportModel.Tittle))
            {
                validationErrors.Add("Title is missing.");
            }
            if (string.IsNullOrEmpty(reportModel.Kunde))
            {
                validationErrors.Add("kunde is missing.");
            }



            if (reportForm.ImageCollectionViewModel.Images.Count <= 1)
            {
                validationErrors.Add("You need to add at least two images.");
            }




            if (reportForm.DataFraOppdragsgiverTableVM.Prøver.Count == 0)
            {
                validationErrors.Add("DataFraOppdragsgiverTable is missing properties.");
            }
            else
            {
                foreach (var prøve in reportForm.DataFraOppdragsgiverTableVM.Prøver)
                {
                    if (string.IsNullOrEmpty(prøve.Overdekningoppgitt) ||
                        string.IsNullOrEmpty(prøve.Dmax) ||
                        string.IsNullOrEmpty(prøve.OverflateOK) ||
                        string.IsNullOrEmpty(prøve.OverflateUK))
                    {
                        validationErrors.Add("DataFraOppdragsgiverTable has missing properties.");
                        break;
                    }
                }
            }

            if (reportForm.DataEtterKuttingOgSlipingTableVM.Prøver.Count == 0)
            {
                validationErrors.Add("DataEtterKuttingOgSlipingTable is missing properties.");
            }
            else
            {
                foreach (var prøve in reportForm.DataEtterKuttingOgSlipingTableVM.Prøver)
                {
                    if (string.IsNullOrEmpty(prøve.Overflatetilstand) ||
                        string.IsNullOrEmpty(prøve.FasthetSammenligning))
                    {
                        validationErrors.Add("DataEtterKuttingOgSlipingTable has missing properties.");
                        break;
                    }
                }
            }

            if (reportForm.ConcreteDensityTableVM.Prøver.Count == 0)
            {
                validationErrors.Add("ConcreteDensityTable is missing properties.");
            }

            if (reportForm.TrykktestingTableVM.Trykketester.Count == 0)
            {
                validationErrors.Add("TrykktestingTable is missing properties.");
            }

            if (validationErrors.Any())
            {
                string errorMessage = "Validation errors:" + Environment.NewLine +
                                      string.Join(Environment.NewLine, validationErrors);
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
