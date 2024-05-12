using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Commands;
using Final_project.Service;
using Firebase.Auth;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace Final_project.ViewModels
{


    public partial class UserInfoVM : ObservableObject
    {
        private readonly FirebaseStore _firebaseStore;

        public ICommand NavigateRoleManagement { get; }
        public ObservableCollection<UserInfo> Users { get; }

        public UserInfoVM(FirebaseAuthProvider firebaseAuthProvider, 
            INavigationService RoleManagementNavigationService, FirebaseStore firebaseStore)
        {
            _firebaseStore = firebaseStore;
            Users = new ObservableCollection<UserInfo>();
            LoadUsersAsync();
            NavigateRoleManagement = new NavigateCommand(RoleManagementNavigationService);

        }

        private async Task LoadUsersAsync()
        {
            try
            {

                var loadedUsers = await _firebaseStore.LoadUsersAsync();
                Users.Clear();



                foreach (var user in loadedUsers.Where(user => user.Role != "Admin"))
                {

                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error loading users: {ex.Message}");
            }
        }

        [RelayCommand]
        public void Delete(UserInfo userInfo)
        {
            if (userInfo != null && Users.Contains(userInfo))
            {

                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to delete the user '{userInfo.FirstName + userInfo.LastName}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);


                if (result == MessageBoxResult.Yes)
                {

                    _firebaseStore.DeleteUserAsync(userInfo.UserId).Wait();
                    Users.Remove(userInfo);
                }
            }
        }

    }
}


