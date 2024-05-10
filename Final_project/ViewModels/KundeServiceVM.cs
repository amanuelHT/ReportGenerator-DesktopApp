using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Service;
using Final_project.Stores;
using Firebase.Auth;
using Microsoft.Win32;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace Final_project.ViewModels
{
    public partial class KundeServiceVM : ObservableObject
    {
        private UserInfoVM _userInfoVM;
        private readonly FirebaseStore _firebaseStore;


        public MessageVM MessageVM { get; private set; }


        public ObservableCollection<UserInfo> Users { get; }

        [ObservableProperty]
        private string _admin;

        [ObservableProperty]
        private UserInfo _selectedUser;






        public bool IsUserSelected => SelectedUser != null;

        public string FirstName => SelectedUser?.FirstName;
        public string LastName => SelectedUser?.LastName;
        public string Reciver => SelectedUser.UserId.ToString();
        public string Role => SelectedUser?.Role;



        public ModalNavigation ModalNavigation { get; }

        public KundeServiceVM(FirebaseStore firebaseStore, FirebaseAuthProvider firebaseAuthProvider, INavigationService roleManagementNavigationService)
        {

            _firebaseStore = firebaseStore;
            _userInfoVM = new UserInfoVM(firebaseAuthProvider, roleManagementNavigationService, firebaseStore);
            MessageVM = new MessageVM(this, firebaseStore);
            Users = new ObservableCollection<UserInfo>();
            LoadUsersAsync();




        }




        private async Task LoadUsersAsync()
        {
            try
            {
                // Load all users from Firebase
                var loadedUsers = await _firebaseStore.LoadUsersAsync();
                Users.Clear();


                // Get the Admin user's ID 
                Admin = loadedUsers.FirstOrDefault(user => user.Role == "Admin")?.UserId;

                // Add only non-admin users to the Users collection
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
        public async void Upload()
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                string filepath = openFileDialog.FileName;
                string filename = Path.GetFileName(filepath);
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
                var stream = File.Open(filepath, FileMode.Open);

                await _firebaseStore.UploadReportAsync(stream, filename);
                await _firebaseStore.AddReportAsync(filenameWithoutExtension);

                //Items.Clear();
            }
        }
    }
}
