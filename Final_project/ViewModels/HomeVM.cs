using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using Final_project.Views;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class HomeVM : ObservableObject
    {
        private readonly NavigationStore _navigationStore;

        public ReportDetailsVM ReportDetailsVM { get; }
        public ReportListVM ReportListVM { get; }
        public ICommand AddReportCommand { get; }
        public ICommand NavigatHomeCommand { get; }

        public HomeVM(
             ModalWindow modalWindow,
             ModalNavigation modalNavigation,
             ReportStore reportStore,
             SelectedReportStore selectedReportStore,
             NavigationStore navigationStore,
            INavigationService navigationService)
        {
            _navigationStore = navigationStore;
            ReportDetailsVM = new ReportDetailsVM(selectedReportStore, reportStore, navigationStore, modalNavigation);
            ReportListVM = ReportListVM.loadViewModel(reportStore, selectedReportStore, navigationStore, modalNavigation);

            AddReportCommand = new OpenAddCommand(modalWindow, modalNavigation, reportStore, navigationStore);

        }

        public static HomeVM LoadHome(ModalWindow modalWindow, ModalNavigation modalNavigation, ReportStore reportStore, 
                                      SelectedReportStore selectedReportStore, NavigationStore navigationStore, INavigationService navigationService)
        {
            HomeVM viewModel = new HomeVM(modalWindow, modalNavigation, reportStore, selectedReportStore, navigationStore, navigationService);

            return viewModel;
        }
    }
}
