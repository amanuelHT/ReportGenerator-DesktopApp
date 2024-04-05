using System.Threading.Tasks;
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
                return;

            try
            {
                await _firebaseAuthProvider.SendPasswordResetEmailAsync(_ResetPassword.Email);
                System.Windows.MessageBox.Show("Password reset email has been sent. Please check your inbox.", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"Failed to send password reset email. {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}
