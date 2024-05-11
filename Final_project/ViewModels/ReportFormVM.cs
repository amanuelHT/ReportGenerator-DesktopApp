using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Stores;
using Final_project.ViewModels.ReportComponentsVM;
using Final_project.ViewModels.TablesVM;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public partial class ReportFormVM : ObservableObject, IDisposable
    {
        private readonly ModalNavigation _modalNavigation;
        public TestUtførtAvVM TestUtførtAvVM { get; set; }
        public KontrollertAvVM KontrollertAvVM { get; set; }

        public ObservableObject CurrentVM => _modalNavigation.CurrentView;
        public bool IsFormOpen => _modalNavigation.IsOpen;

        public List<ReportImageModel> Images { get; set; }

        public ImageVM ImageVM { get; set; }
        public ImageCollectionVM ImageCollectionViewModel { get; set; }



        public TestVM TestVM { get; set; }
        public TestCollectionVM TestCollectionVM { get; set; }

        public VerktøyVM VerktøyVM { get; set; }
        public VerktøyCollectionVM VerktøyCollectionVM { get; set; }


        public DataFraOppdragsgiverTableVM DataFraOppdragsgiverTableVM { get; set; }
        public DataFraOppdragsgiverPrøverVM DataFraOppdragsgiverPrøverVM { get; }

        public DataEtterKuttingOgSlipingTableVM DataEtterKuttingOgSlipingTableVM { get; }
        public DataEtterKuttingOgSlipingPrøveVM DataEtterKuttingOgSlipingPrøveVM { get; }

        public ConcreteDensityTableVM ConcreteDensityTableVM { get; }
        public ConcreteDensityPrøveVM ConcreteDensityPrøveVM { get; }

        public TrykktestingTableVM TrykktestingTableVM { get; }
        public TrykktestingPrøveVM TrykktestingPrøveVM { get; }


        [ObservableProperty]
        private string _tittle;

        [ObservableProperty]
        private bool _status;

        [ObservableProperty]
        private string _kunde;

        [ObservableProperty]
        private string _avvikFraStandarder;

        [ObservableProperty]
        private DateTime _motattDato;

        [ObservableProperty]
        private string _kommentarer;

        [ObservableProperty]
        private int _uiaRegnr;


        public ICommand AddReportTableCommand { get; }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ReportFormVM(ICommand submitCommand, ICommand cancelCommand, ReportStore reportStore, ModalNavigation modalNavigation, Guid reportid)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
            _modalNavigation = modalNavigation;


            ImageCollectionViewModel = new ImageCollectionVM(reportStore, reportid);
            ImageVM = new ImageVM(reportid, ImageCollectionViewModel, reportStore);

            DataFraOppdragsgiverTableVM = new DataFraOppdragsgiverTableVM(reportStore, modalNavigation, reportid);
            DataFraOppdragsgiverPrøverVM = new DataFraOppdragsgiverPrøverVM(modalNavigation, reportid, DataFraOppdragsgiverTableVM);


            DataEtterKuttingOgSlipingTableVM = new DataEtterKuttingOgSlipingTableVM(modalNavigation, reportid);
            DataEtterKuttingOgSlipingPrøveVM = new DataEtterKuttingOgSlipingPrøveVM(modalNavigation, reportid, DataEtterKuttingOgSlipingTableVM);


            ConcreteDensityTableVM = new ConcreteDensityTableVM(modalNavigation, reportid);
            ConcreteDensityPrøveVM = new ConcreteDensityPrøveVM(modalNavigation, reportid, ConcreteDensityTableVM);


            TrykktestingTableVM = new TrykktestingTableVM(modalNavigation, reportid);
            TrykktestingPrøveVM = new TrykktestingPrøveVM(modalNavigation, reportid, TrykktestingTableVM);

            TestCollectionVM = new TestCollectionVM(reportid);
            TestVM = new TestVM(reportid, TestCollectionVM);

            VerktøyCollectionVM = new VerktøyCollectionVM(reportid);
            VerktøyVM = new VerktøyVM(VerktøyCollectionVM, reportid);

            TestUtførtAvVM = new TestUtførtAvVM(reportid, this);
            KontrollertAvVM = new KontrollertAvVM(reportid, this);


            _modalNavigation.CurrentViewChanged += ModalNavigation_CurrentViewChanged;
        }

        private void ModalNavigation_CurrentViewChanged()
        {
            OnPropertyChanged(nameof(CurrentVM));
            OnPropertyChanged(nameof(IsFormOpen));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _modalNavigation.CurrentViewChanged -= ModalNavigation_CurrentViewChanged;
                
            }
            
        }
    }
}
