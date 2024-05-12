using Domain.Models;
using Final_project.ViewModels;
using System.Windows;

namespace Final_project.Other
{
    internal class ReportValidator
    {
        // Validerer rapport skjema
        public static bool IsValidReportModel(ReportModel reportModel, ReportFormVM reportForm)
        {
            List<string> validationErrors = new List<string>();

            if (string.IsNullOrEmpty(reportModel.Tittle))
            {
                validationErrors.Add("Tittel mangler.");
            }

            if (string.IsNullOrEmpty(reportModel.Kunde))
            {
                validationErrors.Add("Kunde mangler.");
            }

            if (string.IsNullOrEmpty(reportModel.AvvikFraStandarder))
            {
                validationErrors.Add("Avvik fra standarder mangler.");
            }

            if (reportModel.MotattDato == default(DateTime))
            {
                validationErrors.Add("Mottaksdato mangler.");
            }

            if (string.IsNullOrEmpty(reportModel.Kommentarer))
            {
                validationErrors.Add("Kommentarer mangler.");
            }

            if (reportModel.UiaRegnr == 0)
            {
                validationErrors.Add("UIA-registreringsnummer mangler.");
            }

            if (reportForm.ImageCollectionViewModel.Images.Count <= 1)
            {
                validationErrors.Add("Du må legge til minst to bilder.");
            }

            if (reportForm.DataFraOppdragsgiverTableVM.Prøver.Count == 0)
            {
                validationErrors.Add("DataFraOppdragsgiverTable mangler egenskaper.");
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
                        validationErrors.Add("DataFraOppdragsgiverTable har manglende egenskaper.");
                        break;
                    }
                }
            }

            if (reportForm.TestCollectionVM.tests.Count == 0)
            {
                validationErrors.Add("Tester utført mangler.");
            }

            if (reportForm.VerktøyCollectionVM.verktøyVMs.Count == 0)
            {
                validationErrors.Add("Benyttet utstyr mangler.");
            }

            if (reportForm.ConcreteDensityTableVM.Prøver.Count == 0)
            {
                validationErrors.Add("ConcreteDensityTable mangler egenskaper.");
            }

            if (reportForm.DataEtterKuttingOgSlipingTableVM.Prøver.Count == 0)
            {
                validationErrors.Add("DataEtterKuttingOgSlipingTable mangler egenskaper.");
            }
            else
            {
                foreach (var prøve in reportForm.DataEtterKuttingOgSlipingTableVM.Prøver)
                {
                    if (string.IsNullOrEmpty(prøve.Overflatetilstand) ||
                        string.IsNullOrEmpty(prøve.FasthetSammenligning))
                    {
                        validationErrors.Add("DataEtterKuttingOgSlipingTable har manglende egenskaper.");
                        break;
                    }
                }
            }

            if (reportForm.ConcreteDensityTableVM.Prøver.Count == 0)
            {
                validationErrors.Add("ConcreteDensityTable mangler egenskaper.");
            }

            if (reportForm.TrykktestingTableVM.Trykketester.Count == 0)
            {
                validationErrors.Add("TrykktestingTable mangler egenskaper.");
            }


            if (string.IsNullOrEmpty(reportForm.KontrollertAvVM.Name))
            {
                validationErrors.Add(" KontrollertAvVM mangler Navn .");
            }

            if (string.IsNullOrEmpty(reportForm.KontrollertAvVM.Name))
            {
                validationErrors.Add("KontrollertAvVM mangler Avdeling .");
            }



            if (string.IsNullOrEmpty(reportForm.KontrollertAvVM.Position))
            {
                validationErrors.Add("KontrollertAvVM mangler Position");
            }


            if (string.IsNullOrEmpty(reportForm.TestUtførtAvVM.Name))
            {
                validationErrors.Add(" Test utført a mangler Navn.");
            }

            if (string.IsNullOrEmpty(reportForm.TestUtførtAvVM.Department))
            {
                validationErrors.Add(" Test utført av mangler Department");
            }


            if (string.IsNullOrEmpty(reportForm.TestUtførtAvVM.Position))
            {
                validationErrors.Add(" Test utført av mangler Position.");
            }






            if (validationErrors.Any())
            {
                string errorMessage = "Feil eller mangler:" + Environment.NewLine +
                                      string.Join(Environment.NewLine, validationErrors);
                MessageBox.Show(errorMessage, "FYLLE UT ALLE FELTER:", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}