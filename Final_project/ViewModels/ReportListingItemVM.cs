using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ReportListingItemVM : ViewModelBase
    {
        public ReportModel ReportModel { get; private set; }

        public string Tittle => ReportModel.Tittle;


        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand EditCommand { get; }

        public ICommand GenerateReport { get; }

        public ICommand DeleteCommand { get; }
        public ReportStore ReportStore { get; }

        public ModalNavigation ModalNavigation { get; }



        public ReportListingItemVM(
            ReportModel reportModel,
            ReportStore reportStore, NavigationStore navigationStore,
            ModalNavigation modalNavigation)
        {

            ReportModel = reportModel;
            EditCommand = new OpenEditCommand(this, reportStore, navigationStore, modalNavigation);
            DeleteCommand = new DeleteReportCommand(this, reportStore);


        }

        internal void UpdateReport(ReportModel reportModel)
        {
            ReportModel = reportModel;
            OnPropertyChanged(nameof(Tittle));
        }
    }
}