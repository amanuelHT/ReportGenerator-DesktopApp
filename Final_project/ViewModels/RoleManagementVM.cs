using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Service;
using Firebase.Auth;
using System.Windows.Input;
using System.Windows.Media;

namespace Final_project.ViewModels
{

    public partial class RoleManagementVM : ObservableObject
    {


        public ICommand NavigateUserinfo { get; }
        public RoleManagementVM(FirebaseAuthProvider firebaseAuthProvider, INavigationService navigationservice)
        {
            SaveCommand = new RoleManagementCommand(this, firebaseAuthProvider);
            CancelCommand = new RelayCommand(_ => ClearInputs());
            NavigateUserinfo = new NavigateCommand(navigationservice);

        }

        [ObservableProperty]
        private string _firstName;


        [ObservableProperty]
        private string _lastName;


        [ObservableProperty]
        private DateTime? _birthDate;

        [ObservableProperty]
        private string _selectedRole;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;



        [ObservableProperty]
        private string _repeatPassword;


        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        public Brush _messageBrush;


        public void SetMessage(string message, bool isSuccess)
        {
            Message = message;
            MessageBrush = isSuccess ? Brushes.Green : Brushes.Red;
        }


        [ObservableProperty]
        private ICommand _saveCommand;

        [ObservableProperty]
        private ICommand _cancelCommand;





        private void ClearInputs()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = null;
            Email = string.Empty;
            Password = string.Empty;
            RepeatPassword = string.Empty;
            SelectedRole = string.Empty;
            Message = string.Empty;
        }





    }
}
