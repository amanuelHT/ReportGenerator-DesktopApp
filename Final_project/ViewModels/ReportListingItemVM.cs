using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public partial class ReportListingItemVM : ObservableObject
    {
        public ReportModel ReportModel { get; private set; }

        public string Tittle => ReportModel.Tittle;

        [ObservableProperty]
        private bool _isDeleting;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasErrorMessage))]
        private string _errorMessage;

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand EditCommand { get; }

        public ICommand GenerateReport { get; }

        public ReportStore ReportStore { get; }

        public ModalNavigation ModalNavigation { get; }


        public ReportListingItemVM(
            ReportModel reportModel,
            ReportStore reportStore,
            ModalNavigation navigationStore)
        {
            ReportModel = reportModel;
            EditCommand = new OpenEditCommand(this, reportStore, navigationStore);
            ReportStore = reportStore;
        }

        [RelayCommand]
        private async Task Delete()
        {
            ErrorMessage = null;
            IsDeleting = true;

            try
            {
                await ReportStore.Delete(ReportModel.Id);
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to delete. Please try again later.";
            }
            finally
            {
                IsDeleting = false;
            }
        }



        internal void UpdateReport(ReportModel reportModel)
        {
            ReportModel = reportModel;
            OnPropertyChanged(nameof(Tittle));
        }
    }
}
