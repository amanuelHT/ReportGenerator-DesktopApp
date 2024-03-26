using Final_project.Service;
using Final_project.Stores;
using Final_project.ViewModels;
using Report_Generator_Domain.Models;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Final_project.Commands
{

    public class LogInCommand : AsyncCommandBase
    {

        private readonly LoginVM _login;
        private readonly AccountStore _accountStore;
        private readonly FirebaseAuthProvider _firebaseAuthProvider;
        public INavigationService _navigationService { get; }

        public LogInCommand(LoginVM login, AccountStore accountStore, INavigationService navigationService, FirebaseAuthProvider firebaseAuthProvider)
        {
            _login = login;
            _accountStore = accountStore;
            _navigationService = navigationService;
            _firebaseAuthProvider = firebaseAuthProvider;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                var auth = await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(_login.Username, _login.Password);
                // Assuming you want to create an Account object on successful login
                Account account = new Account()
                {
                    Email = auth.User.Email,
                    Username = auth.User.Email, 
                    // Or another identifier if you have it
                };

                _accountStore.CurrentAccount = account;
                //MessageBox.Show("LogIn successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _navigationService.Navigate();
            }
            catch (Exception)
            {
                MessageBox.Show("LogIn failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
