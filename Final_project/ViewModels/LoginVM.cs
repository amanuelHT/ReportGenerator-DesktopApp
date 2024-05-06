using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using Firebase.Auth;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public partial class LoginVM : ObservableObject
    {
        private NavigationStore _navigationStore;
        private readonly FirebaseAuthProvider _firebaseAuthProvider;

        public string name => "Log";
        public ICommand NavigateResetPassword { get; }
        public ICommand LogInCommand { get; }


        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;


        public LoginVM(AccountStore accountStore,
            INavigationService accountNavigationService, FirebaseAuthProvider firebaseAuthProvider, INavigationService ResetPasswordNavigarionService)
        {


            LogInCommand = new LogInCommand(this, accountStore, accountNavigationService, firebaseAuthProvider);
            NavigateResetPassword = new NavigateCommand(ResetPasswordNavigarionService);
        }
    }
}
