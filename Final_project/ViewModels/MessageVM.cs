using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Final_project.ViewModels
{
    public partial class MessageVM : ObservableObject
    {
        private readonly KundeServiceVM _kundeServiceVM;
        private readonly FirebaseStore _firebaseStore;

        public ObservableCollection<MessageModel> Messages { get; } = new ObservableCollection<MessageModel>();

        public MessageVM(KundeServiceVM kundeServiceVM, FirebaseStore firebaseStore)
        {
            _kundeServiceVM = kundeServiceVM ?? throw new ArgumentNullException(nameof(kundeServiceVM));
            _firebaseStore = firebaseStore;

            LoadMessages();

            _kundeServiceVM.PropertyChanged += SubscribeToSelectedUserChanges;

        }




        private void SubscribeToSelectedUserChanges(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_kundeServiceVM.SelectedUser))
            {
                OnPropertyChanged(nameof(FilteredMessages));
            }
        }
        public UserInfo SelectedUser => _kundeServiceVM.SelectedUser;




        public ObservableCollection<MessageModel> FilteredMessages
        {
            get
            {
                // If no user is selected
                // return an empty collection initially
                if (SelectedUser == null)
                    return new ObservableCollection<MessageModel>();
                else
                    return new ObservableCollection<MessageModel>(
                        Messages.Where(m => m.Receiver == SelectedUser.UserId.ToString())
                    );
            }
        }

        private async Task LoadMessages()
        {
            try
            {
                var loadmessages = await _firebaseStore.LoadMessages();


                Messages.Clear();

                foreach (var message in loadmessages)
                {
                    Messages.Add(message);
                }


                OnPropertyChanged(nameof(FilteredMessages));
            }

            catch (Exception ex)
            {

                Debug.WriteLine($"Error loading messages: {ex.Message}");
            }
        }



        [ObservableProperty]
        private string _content;



        [ObservableProperty]
        private string _fileName;




        [RelayCommand]
        private async void SendMessage()
        {

            if (_kundeServiceVM.SelectedUser == null)
            {
                MessageBox.Show("Please select a user.");
                return;

            }

            if (string.IsNullOrWhiteSpace(this.Content) && string.IsNullOrWhiteSpace(this.FileName))
            {
                MessageBox.Show("Empty content Message ", "Empty Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var message = new MessageModel
                {
                    Content = this.Content,
                    Sender = _kundeServiceVM.Admin,
                    Receiver = _kundeServiceVM.SelectedUser.UserId,
                    Filepath = this.FileName,
                    Timestamp = Google.Cloud.Firestore.Timestamp.FromDateTime(DateTime.UtcNow)
                };


                await _firebaseStore.SendMessageAsync(message);
                Messages.Add(message);

                OnPropertyChanged(nameof(FilteredMessages));

                this.Content = string.Empty;
                this.FileName = string.Empty;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }


        [RelayCommand]
        public async void Upload()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                try
                {
                    string filepath = openFileDialog.FileName;
                    string filename = Path.GetFileName(filepath);
                    string filenameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);

                    using (var stream = File.Open(filepath, FileMode.Open))
                    {
                        if (SelectedUser == null || string.IsNullOrEmpty(SelectedUser.UserId))
                        {
                            MessageBox.Show("Please select a user before uploading.", "User Not Selected", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        await _firebaseStore.UploadReportMessageAsync(stream, filename, SelectedUser.UserId);

                        FileName = filenameWithoutExtension;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No file selected.", "Cancelled Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
