using Firebase.Auth;
using Final_project.Commands;
using System.Windows.Input;
using Final_project.Service;

namespace Final_project.ViewModels
{
    public class ResetPasswordVM : ViewModelBase
    {
        private readonly FirebaseAuthProvider _firebaseAuthProvider;
        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public ICommand ResetPasswordCommand { get; }
        public ICommand CancelCommand { get; }

        public ResetPasswordVM(FirebaseAuthProvider firebaseAuthProvider, INavigationService LoginNavigarionService)
        {
            _firebaseAuthProvider = firebaseAuthProvider;
            ResetPasswordCommand = new ResetPasswordCommand(this, _firebaseAuthProvider);
            CancelCommand = new NavigateCommand(LoginNavigarionService);

        }
    }
}
