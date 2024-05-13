using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ReportDetailsVM : ObservableObject, IDisposable
    {
        private readonly SelectedReportStore _selectedReportStore;
        private readonly ReportStore _reportStore;

        private ReportModel SelectedReport => _selectedReportStore.SelectedReport;


        public bool HasReportSelected => SelectedReport != null;
        public string Tittle => SelectedReport?.Tittle ?? "Ukjent";
        public string Status => SelectedReport?.Status == true ? "Fullført" : "Ikke Fullført";
        public string Kunde => SelectedReport?.Kunde ?? "No navn";

        public Guid ReportId => SelectedReport?.Id ?? Guid.Empty;



        public ICommand EditCommand { get; }

        public ICommand GenerateReport { get; }

        public ICommand DeleteCommand { get; }


        public ReportDetailsVM(SelectedReportStore selectedReportStore, ReportStore reportStore, NavigationStore navigationStore, ModalNavigation modalNavigation)
        {
            _selectedReportStore = selectedReportStore;
            _reportStore = reportStore;

            _selectedReportStore.SelectedReportChanged += _selectedReportStore_SelectedReportChanged;

            EditCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(() =>
                new OpenEditCommand(ReportId, reportStore, navigationStore, modalNavigation).Execute(null)
            );

            DeleteCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(() =>
            {
                Guid currentReportId = ReportId;
                if (currentReportId != Guid.Empty)
                {
                    new DeleteReportCommand(currentReportId, reportStore).Execute(null);
                }
            });
        }




        public void Dispose()
        {
            _selectedReportStore.SelectedReportChanged -= _selectedReportStore_SelectedReportChanged;

        }
        private void _selectedReportStore_SelectedReportChanged()
        {
            OnPropertyChanged(nameof(HasReportSelected));
            OnPropertyChanged(nameof(Tittle));
            OnPropertyChanged(nameof(Status));
            OnPropertyChanged(nameof(Kunde));
            OnPropertyChanged(nameof(ReportId));



        }
    }
}