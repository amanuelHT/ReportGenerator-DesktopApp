using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Firebase.Auth;
using System.Windows.Input;
using Final_project.Service;

namespace Final_project.ViewModels
{
    public partial class ResetPasswordVM : ObservableObject
    {
        private readonly FirebaseAuthProvider _firebaseAuthProvider;


        [ObservableProperty]
        private string _email;

        //public string Email
        //{
        //    get => _email;
        //    set
        //    {
        //        _email = value;
        //        OnPropertyChanged(nameof(Email));
        //    }
        //}

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
