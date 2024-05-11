using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public partial class ReportViewerVM : ObservableObject
    {
        private readonly ReportStore _reportStore;
        public ICommand NavigateGeneratedReportCommand { get; }
        public ObservableCollection<ReportModel> AvailableReports { get; }

        [ObservableProperty]
        private ReportModel _selectedReport;

        partial void OnSelectedReportChanged(ReportModel value)
        {
            LoadSelectedReport();
        }


        public ReportModel SelectedReportData { get; private set; }
        public TestUtførtAvModel TestUtførtAvModels { get; private set; }

        public KontrollertAvførtAvModel KontrollertAvførtAvModels { get; private set; }



        public ObservableCollection<ReportImageModel> ReportImages { get; private set; }
        public ObservableCollection<TestModel> TestModels { get; private set; }
        public ObservableCollection<verktøyModel> VerktøyModels { get; private set; }

        public ObservableCollection<TrykktestingModel> TrykktestingModels { get; private set; }
        public ObservableCollection<DataEtterKuttingOgSlipingModel> DataEtterKuttingOgSlipingModels { get; private set; }
        public ObservableCollection<DataFraOppdragsgiverPrøverModel> DataFraOppdragsgiverPrøverModels { get; private set; }
        public ObservableCollection<ConcreteDensityModel> ConcreteDensityModels { get; private set; }


        public ReportViewerVM(ReportStore reportStore, INavigationService generatedReportNavigationService)
        {
            _reportStore = reportStore;
            NavigateGeneratedReportCommand = new NavigateCommand(generatedReportNavigationService);
            AvailableReports = new ObservableCollection<ReportModel>(_reportStore.ReportModels);
        }

        private async Task LoadSelectedReport()
        {
            if (_selectedReport == null) return;

            (ReportModel reportData, TestUtførtAvModel testUtførtAvModel, KontrollertAvførtAvModel KontrollertAvførtAvModel,
            List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels,
            List<ReportImageModel> images,
            List<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels,
            List<ConcreteDensityModel> concreteDensityModels,
            List<TrykktestingModel> trykktestingModels,
            List<TestModel> tests,
            List<verktøyModel> verktøyer) = await _reportStore.GetReportData(_selectedReport.Id);

            if (reportData != null)
            {
                SelectedReportData = reportData;
                OnPropertyChanged(nameof(SelectedReportData));

                ReportImages = new ObservableCollection<ReportImageModel>(images);
                OnPropertyChanged(nameof(ReportImages));

                TestModels = new ObservableCollection<TestModel>(tests);
                OnPropertyChanged(nameof(TestModels));

                VerktøyModels = new ObservableCollection<verktøyModel>(verktøyer);
                OnPropertyChanged(nameof(VerktøyModels));


                KontrollertAvførtAvModels = KontrollertAvførtAvModel;
                OnPropertyChanged(nameof(KontrollertAvførtAvModels));


                TestUtførtAvModels = testUtførtAvModel;
                OnPropertyChanged(nameof(TestUtførtAvModels));


                TrykktestingModels = new ObservableCollection<TrykktestingModel>(trykktestingModels);
                OnPropertyChanged(nameof(TrykktestingModels));



                DataEtterKuttingOgSlipingModels = new ObservableCollection<DataEtterKuttingOgSlipingModel>(dataEtterKuttingOgSlipingModels);
                OnPropertyChanged(nameof(DataEtterKuttingOgSlipingModels));

                DataFraOppdragsgiverPrøverModels = new ObservableCollection<DataFraOppdragsgiverPrøverModel>(dataFraOppdragsgiverPrøverModels);
                OnPropertyChanged(nameof(DataFraOppdragsgiverPrøverModels));

                ConcreteDensityModels = new ObservableCollection<ConcreteDensityModel>(concreteDensityModels);
                OnPropertyChanged(nameof(ConcreteDensityModels));







            }
        }
    }
}
