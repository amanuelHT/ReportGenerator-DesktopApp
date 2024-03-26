using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ReportFormVM(ICommand submitCommand, ICommand cancelCommand) : ViewModelBase
    {


        // Private fields for properties
        private string _tittle;
        private bool _status;
        private string _kunde;



        // Public properties with property change notification
        public string Tittle
        {
            get { return _tittle; }
            set
            {

                _tittle = value;
                OnPropertyChanged(nameof(Tittle));
                OnPropertyChanged(nameof(CanSubmit));


            }
        }

        public bool Status
        {
            get { return _status; }
            set
            {

                _status = value;
                OnPropertyChanged(nameof(Status));

            }
        }

        public string Kunde
        {
            get { return _kunde; }
            set
            {
                _kunde = value;
                OnPropertyChanged(nameof(Kunde));

            }
        }

        public bool CanSubmit => !string.IsNullOrEmpty(Tittle);
        public ICommand SubmitCommand { get; } = submitCommand;
        public ICommand CancelCommand { get; } = cancelCommand;
    }
}
