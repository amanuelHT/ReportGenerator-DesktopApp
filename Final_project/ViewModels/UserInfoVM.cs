using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Commands;
using Final_project.Service;
using Final_project.Stores;
using Firebase.Auth;
using Google.Cloud.Firestore;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Final_project.ViewModels
{

    // ViewModelBase should implement INotifyPropertyChanged
    public partial class UserInfoVM : ObservableObject
    {

        public ICommand NavigateRoleManagement { get; }
        public ObservableCollection<UserInfo> Users { get; }

        public UserInfoVM(FirebaseAuthProvider firebaseAuthProvider, INavigationService RoleManagementNavigationService)
        {
            Users = new ObservableCollection<UserInfo>();
            LoadUsersAsync(firebaseAuthProvider);
            NavigateRoleManagement = new NavigateCommand(RoleManagementNavigationService);

        }

        public async Task LoadUsersAsync(FirebaseAuthProvider firebaseAuthProvider)
        {
            try
            {
                // Assuming FirestoreHelper.Database has already been initialized
                CollectionReference usersRef = FirestoreHelper.Database.Collection("users");
                QuerySnapshot usersSnapshot = await usersRef.GetSnapshotAsync();
                foreach (var doc in usersSnapshot.Documents)
                {
                    UserInfo userInfo = doc.ConvertTo<UserInfo>();
                    Users.Add(userInfo);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, possibly logging them or informing the user
            }
        }



    }
}


