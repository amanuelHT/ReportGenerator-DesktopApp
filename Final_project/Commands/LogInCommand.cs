using Final_project.Service;
using Final_project.Stores;
using Final_project.ViewModels;
using Firebase.Auth;
using Report_Generator_Domain.Models;
using System.Windows;
using UserInfo = Report_Generator_Domain.Models.UserInfo;

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

                // Retrieve the role from Firestore
                var userDocument = await FirestoreHelper.Database
                  .Collection("users")
                  .Document(auth.User.LocalId)
                  .GetSnapshotAsync();

                if (userDocument.Exists)
                {
                    var userInfo = userDocument.ConvertTo<UserInfo>();

                    // Check if user has the correct role
                    if (userInfo.Role == "Admin" || userInfo.Role == "User")
                    {
                        Account account = new Account()
                        {
                            Email = auth.User.Email,
                            Username = auth.User.Email,
                            Role = userInfo.Role
                        };

                        _accountStore.CurrentAccount = account;
                        _navigationService.Navigate();
                    }
                    else
                    {
                        MessageBox.Show("Access Denied: User does not have the necessary permissions.", "Authorization Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("User data not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LogIn failed: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
