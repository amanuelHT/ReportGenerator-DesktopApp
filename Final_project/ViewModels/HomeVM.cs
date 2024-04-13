using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class HomeVM : ObservableObject
    {
        private readonly ModalNavigation _navigationStore;
        public ObservableObject CurrentVM => _navigationStore.CurrentView;
        public bool IsFormOpen => _navigationStore.IsOpen;

        public ReportDetailsVM ReportDetailsVM { get; }
        public ReportListVM ReportListVM { get; }
        public ICommand AddReportCommand { get; }
        public ICommand NavigatHomeCommand { get; }

        public HomeVM(ReportStore reportStore, SelectedReportStore selectedReportStore,
             ModalNavigation navigationStore,
            INavigationService navigationService)
        {
            _navigationStore = navigationStore;
            ReportDetailsVM = new ReportDetailsVM(selectedReportStore);
            ReportListVM = ReportListVM.loadViewModel(reportStore, selectedReportStore, navigationStore);

            AddReportCommand = new OpenAddCommand(reportStore, navigationStore);


            _navigationStore.CurrentViewChanged += ModalNavigation_CurrentViewChanged;
        }

        private void ModalNavigation_CurrentViewChanged()
        {
            OnPropertyChanged(nameof(CurrentVM));
            OnPropertyChanged(nameof(IsFormOpen));
        }

        //public override void Dispose()
        //{
        //    _navigationStore.CurrentViewChanged -= ModalNavigation_CurrentViewChanged;
        //    base.Dispose();
        //}


        public static HomeVM LoadHome(ReportStore reportStore, SelectedReportStore selectedReportStore, ModalNavigation navigationStore, INavigationService navigationService)
        {
            HomeVM viewModel = new HomeVM(reportStore, selectedReportStore, navigationStore, navigationService);

            //viewModel.HomeVMCommand.Execute(null);
            //fetene
            return viewModel;
        }
    }
}
