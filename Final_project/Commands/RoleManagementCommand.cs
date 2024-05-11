using Final_project.Commands;
using Final_project.Stores;
using Final_project.ViewModels;
using Firebase.Auth;
using Google.Cloud.Firestore;

public class RoleManagementCommand : AsyncCommandBase
{
    private readonly RoleManagementVM _viewModel;
    private readonly FirebaseAuthProvider _firebaseAuthProvider;

    public RoleManagementCommand(RoleManagementVM viewModel, FirebaseAuthProvider firebaseAuthProvider)
    {
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        _firebaseAuthProvider = firebaseAuthProvider ?? throw new ArgumentNullException(nameof(firebaseAuthProvider));
    }


    public override async Task ExecuteAsync(object parameter)
    {
      

        if (_viewModel.Password != _viewModel.RepeatPassword)
        {
            _viewModel.SetMessage("Passwords do not match.", false);
            return;
        }


        if (!ValidateInputs())
        {
            _viewModel.SetMessage("Please fill out all required fields.", false);
            return;
        }

        try
        {
           
            var authLink = await _firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(_viewModel.Email, _viewModel.Password);

            var additionalUserData = new
            {
                FirstName = _viewModel.FirstName,
                LastName = _viewModel.LastName,
                BirthDate = _viewModel.BirthDate?.ToString("yyyy-MM-dd"),
                Role = _viewModel.SelectedRole
            };

            DocumentReference docRef = FirestoreHelper.Database.Collection("users").Document(authLink.User.LocalId);
            await docRef.SetAsync(additionalUserData);

            _viewModel.SetMessage("Registration successful.", true);
        }
        catch (FirebaseAuthException faex) when (faex.Reason == AuthErrorReason.EmailExists)
        {
            _viewModel.SetMessage("Registration failed: Email already exists.", false);
        }
        catch (FirebaseAuthException faex)
        {
            _viewModel.SetMessage($"Registration failed: {faex.Reason}", false);
        }
        catch (Exception ex)
        {
            _viewModel.SetMessage("Registration failed. Please try again later.", false);
        }
    }


    private bool ValidateInputs()
    {
        // Check if any of the required inputs are null, empty, or consist only of white-space characters
        if (string.IsNullOrWhiteSpace(_viewModel.FirstName) ||
            string.IsNullOrWhiteSpace(_viewModel.LastName) ||
            string.IsNullOrWhiteSpace(_viewModel.Email) ||
            string.IsNullOrWhiteSpace(_viewModel.Password) ||
            _viewModel.BirthDate == null ||
            string.IsNullOrWhiteSpace(_viewModel.SelectedRole))
        {
            return false;
        }
        // Add any additional validation logic here if necessary
        return true;
    } //just tryning
}
