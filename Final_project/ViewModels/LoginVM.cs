using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using Firebase.Auth;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private readonly FirebaseAuthProvider _firebaseAuthProvider;

        public string name => "Log";
        public ICommand NavigateAccountCommand { get; }
        public ICommand LogInCommand { get; }



        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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

        public LoginVM(AccountStore accountStore,
            INavigationService accountNavigationService, FirebaseAuthProvider firebaseAuthProvider)
        {


            LogInCommand = new LogInCommand(this, accountStore, accountNavigationService, firebaseAuthProvider);

        }
    }
}
