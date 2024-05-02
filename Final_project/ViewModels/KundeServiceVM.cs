using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_project.Service;
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

        // Selected user to whom the message will be sent
        private UserInfo _selectedUser;

        public ObservableCollection<UserInfo> Users { get; } = new ObservableCollection<UserInfo>();
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

        [ObservableProperty]
        public string _content;

        [ObservableProperty]
        public UserInfo _sender;


        [RelayCommand]
        private void SendMessage()
        {
            // Ensure a user is selected
            if (SelectedUser == null)
            {
                // Handle the case where no user is selected
                return;
            }

            try
            {
                // Create a new instance of MessageModel with the necessary parameters
                var message = new MessageModel(Content, Sender, SelectedUser);

                // Logic to send the message

                // Once the message is sent, you might want to clear the input box
                // and update the UI accordingly
                //MessageInputBox.Text = ""; // Clear the input box

                // You might also want to update the chat history immediately
                // instead of waiting for the next refresh cycle
                // ChatHistory += $"{_sender.Name}: {_content}\n";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during message sending
            }
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
        public string Role => SelectedUser?.Role;

        public KundeServiceVM(FirebaseStore firebaseStore, FirebaseAuthProvider firebaseAuthProvider, INavigationService roleManagementNavigationService)
        {
            _firebaseStore = firebaseStore;
            _userInfoVM = new UserInfoVM(firebaseAuthProvider, roleManagementNavigationService);
            LoadUsersAsync(firebaseAuthProvider);
        }


        //[RelayCommand]
        //private async void SendDocument()
        //{
        //    // Create an instance of UserInfoVM
        //    UserInfoVM userInfoVM = new UserInfoVM(firebaseAuthProvider, NavigationService);
        //    //KundeServiceVM kundeServiceVM = new KundeServiceVM(_firebaseStore, firebaseAuthProvider, NavigationService);


        //    // Load users into UserInfoVM
        //    await userInfoVM.LoadUsersAsync(firebaseAuthProvider);

        //    // Add users to the Users collection of GeneratedReportListVM
        //    foreach (var user in userInfoVM.Users)
        //    {
        //        Users.Add(user);
        //    }







        private async Task LoadUsersAsync(FirebaseAuthProvider firebaseAuthProvider)
        {
            try
            {
                await _userInfoVM.LoadUsersAsync(firebaseAuthProvider);
                foreach (var user in _userInfoVM.Users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
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

                Items.Clear();
            }
        }
    }
}
