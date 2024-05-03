using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Service;
using Final_project.Stores;
using Firebase.Auth;
using Microsoft.Win32;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace Final_project.ViewModels
{
    public partial class KundeServiceVM : ObservableObject
    {
        private UserInfoVM _userInfoVM;
        private readonly FirebaseStore _firebaseStore;
        private string _admin;

        public MessageVM MessageVM { get; private set; }

        // Selected user to whom the message will be sent
        private UserInfo _selectedUser;

        public ObservableCollection<UserInfo> Users { get; } = new ObservableCollection<UserInfo>();
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();


        public string Admin
        {
            get => _admin;
            set => SetProperty(ref _admin, value);
        }

        public UserInfo SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                OnPropertyChanged(nameof(IsUserSelected));
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(Role));
            }
        }

        public bool IsUserSelected => SelectedUser != null;

        public string FirstName => SelectedUser?.FirstName;
        public string LastName => SelectedUser?.LastName;
        public string Reciver => SelectedUser.UserId.ToString();
        public string Role => SelectedUser?.Role;

        public ModalNavigation ModalNavigation { get; }

        public KundeServiceVM(FirebaseStore firebaseStore, FirebaseAuthProvider firebaseAuthProvider, INavigationService roleManagementNavigationService)
        {

            _firebaseStore = firebaseStore;
            _userInfoVM = new UserInfoVM(firebaseAuthProvider, roleManagementNavigationService);
            MessageVM = new MessageVM(this, firebaseStore);
            LoadUsersAsync(firebaseAuthProvider);
        }

        private async Task LoadUsersAsync(FirebaseAuthProvider firebaseAuthProvider)
        {
            try
            {
                await _userInfoVM.LoadUsersAsync(firebaseAuthProvider);
                foreach (var user in _userInfoVM.Users)
                {
                    Users.Add(user);
                }

                Admin = GetAdminUserId();
            }
            catch (Exception ex)
            {
                // Handle any exceptions
            }
        }


        private string GetAdminUserId()
        {
            return Users.FirstOrDefault(user => user.Role == "Admin")?.UserId;
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

                Items.Clear();
            }
        }
    }
}
