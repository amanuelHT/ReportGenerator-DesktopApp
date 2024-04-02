using Domain.Models;
using Final_project.Commands;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ReportFormVM : ViewModelBase
    {
        private string _tittle;
        private bool _status;
        private string _kunde;

        public List<ReportImageModel> Images { get; set; }

        public ImageCollectionVM ImageCollectionViewModel { get; }
        public DeleteImageCommand DeleteImageCommand { get; set; }

        public string Tittle
        {
            get => _tittle;
            set
            {
                _tittle = value;
                OnPropertyChanged(nameof(Tittle));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public bool Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string Kunde
        {
            get => _kunde;
            set
            {
                _kunde = value;
                OnPropertyChanged(nameof(Kunde));
            }
        }

        public bool CanSubmit => !string.IsNullOrEmpty(Tittle);

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        // Modify constructor to accept List<ImageVM>
        public ReportFormVM(ICommand submitCommand, ICommand cancelCommand, ReportStore reportStore)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
            Images = new List<ReportImageModel>();



            ImageCollectionViewModel = new ImageCollectionVM(reportStore, this);
        }
    }
}
