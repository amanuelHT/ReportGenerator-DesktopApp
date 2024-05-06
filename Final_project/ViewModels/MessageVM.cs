using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

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
                OnPropertyChanged(nameof(FilteredMessages)); // Notify the UI to update filtered messages
            }
        }
        public UserInfo SelectedUser => _kundeServiceVM.SelectedUser;



        public ObservableCollection<MessageModel> FilteredMessages
        {
            get
            {
                // If no user is selected, return an empty collection initially
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

                // Clear existing messages before adding the loaded ones
                Messages.Clear();

                // Add loaded messages to the Messages collection
                foreach (var message in loadmessages)
                {
                    Messages.Add(message);
                }

                // Update filtered messages
                OnPropertyChanged(nameof(FilteredMessages));
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"Error loading messages: {ex.Message}");
            }
        }







        [ObservableProperty]
        private string _content;

        //public string Content
        //{
        //    get => _content;
        //    set => SetProperty(ref _content, value);
        //}

        [ObservableProperty]
        private string _fileName;

        //public string FileName
        //{
        //    get => _fileName;
        //    set => SetProperty(ref _fileName, value);
        //}




        [RelayCommand]
        private async void SendMessage()
        {
            if (_kundeServiceVM.SelectedUser == null)
            {
                return;
            }

            try
            {
                // Create the message model
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



                await _firebaseStore.UploadReportMessageAsync(stream, filename);

                FileName = filenameWithoutExtension;



            }
        }
    }
}
