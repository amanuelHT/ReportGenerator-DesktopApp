using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Firebase.Auth;
using System.Windows.Input;
using System.Windows.Media;

namespace Final_project.ViewModels
{

    public partial class RoleManagementVM : ObservableObject
    {


        public RoleManagementVM(FirebaseAuthProvider firebaseAuthProvider)
        {
            // Initialize SaveCommand with the RoleManagementCommand
            SaveCommand = new RoleManagementCommand(this, firebaseAuthProvider);
            CancelCommand = new RelayCommand(_ => ClearInputs());

        }

        [ObservableProperty]
        private string _firstName;


        [ObservableProperty]
        private string _lastName;


        //private string _birthDate;
        //public string BirthDate
        //{
        //    get => _birthDate;
        //    set
        //    {
        //        if (_birthDate != value)
        //        {
        //            _birthDate = value;
        //            OnPropertyChanged(nameof(BirthDate));
        //        }
        //    }
        //}

        private DateTime? _birthDate;
        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged(nameof(BirthDate));
                }
            }
        }


        private string _selectedRole;
        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    OnPropertyChanged(nameof(SelectedRole));
                }
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }





        private string _repeatPassword;

        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                if (_repeatPassword != value)
                {
                    _repeatPassword = value;
                    OnPropertyChanged(nameof(RepeatPassword));
                    // Optionally validate passwords immediately as the user types
                    //ValidatePasswords();
                }
            }
        }

        //private void ValidatePasswords()
        //{
        //    if (Password != RepeatPassword)
        //    {
        //        SetMessage("Passwords do not match.", false);
        //    }
        //    else
        //    {
        //        // Optionally clear the message if they match,
        //        // or leave it as is if you only want to validate on save
        //        SetMessage(string.Empty, true);
        //    }
        //}



        private string _message;
        private Brush _messageBrush = Brushes.Black; // Default message color

        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public Brush MessageBrush
        {
            get => _messageBrush;
            set
            {
                if (_messageBrush != value)
                {
                    _messageBrush = value;
                    OnPropertyChanged(nameof(MessageBrush));
                }
            }
        }


        public void SetMessage(string message, bool isSuccess)
        {
            Message = message;
            MessageBrush = isSuccess ? Brushes.Green : Brushes.Red;
        }


        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get => _saveCommand;
            set
            {
                if (_saveCommand != value)
                {
                    _saveCommand = value;
                    OnPropertyChanged(nameof(SaveCommand));
                }
            }
        }


        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get => _cancelCommand;
            set
            {
                if (_cancelCommand != value)
                {
                    _cancelCommand = value;
                    OnPropertyChanged(nameof(CancelCommand));
                }
            }
        }




        private void ClearInputs()
        {
            // Reset all the properties to their default values
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = null; // Assuming you want to clear the birth date as well
            Email = string.Empty;
            Password = string.Empty;
            RepeatPassword = string.Empty;
            SelectedRole = string.Empty;
            Message = string.Empty; // Optionally clear any existing message
            // You do not need to reset MessageBrush because it will be set based on the next operation
        }


        // Repeat for CancelCommand, etc.

        // Implement any additional logic or methods needed for your ViewModel here.

        // Override the Dispose method if needed to clean up resources
        //public override void Dispose()
        //{
        //    // Clean up any resources or subscriptions here if necessary
        //    base.Dispose();
        //}
    }
}
