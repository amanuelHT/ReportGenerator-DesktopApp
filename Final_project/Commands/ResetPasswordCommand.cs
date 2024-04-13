using System.Threading.Tasks;
using System.Windows;
using BoldReports.UI.Xaml;
using Final_project.ViewModels;
using Firebase.Auth;

namespace Final_project.Commands
{
    public class ResetPasswordCommand : AsyncCommandBase
    {
        private readonly FirebaseAuthProvider _firebaseAuthProvider;
        private readonly ResetPasswordVM _ResetPassword;

        public ResetPasswordCommand(ResetPasswordVM ResetPassword, FirebaseAuthProvider firebaseAuthProvider)
        {
            _ResetPassword = ResetPassword;
            _firebaseAuthProvider = firebaseAuthProvider;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (string.IsNullOrWhiteSpace(_ResetPassword.Email))
            {
                MessageBox.Show("Please enter an email address.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _firebaseAuthProvider.SendPasswordResetEmailAsync(_ResetPassword.Email);
                
            }
            catch (FirebaseAuthException ex)
            {

                Console.WriteLine($"FirebaseAuthException: Error sending password reset email to {_ResetPassword.Email}. Reason: {ex.Reason} Message: {ex.Message}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"General exception occurred during password reset: {ex.Message}");
            }
            MessageBox.Show("A password reset link will be sent if the email provided is associated with an account.", "Password Reset", MessageBoxButton.OK, MessageBoxImage.Information);
          
        }
       
    }
}
