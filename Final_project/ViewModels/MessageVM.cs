using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Report_Generator_Domain.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace Final_project.ViewModels
{
    public partial class MessageVM : ObservableObject
    {
        private KundeServiceVM _kundeServiceVM;
        private readonly FirebaseStore _firebaseStore;
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

        public MessageVM(KundeServiceVM kundeServiceVM, FirebaseStore firebaseStore)
        {
            _kundeServiceVM = kundeServiceVM ?? throw new ArgumentNullException(nameof(kundeServiceVM));
            _firebaseStore = firebaseStore;
            _kundeServiceVM.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(_kundeServiceVM.SelectedUser))
                {
                    // Perform any necessary actions when the selected user changes
                    // For example, you can update UI or perform validations
                    OnPropertyChanged(nameof(SelectedUser));
                }
            };
        }

        public UserInfo SelectedUser => _kundeServiceVM.SelectedUser;


        private string _content;

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        [RelayCommand]
        private async void SendMessage()
        {
            if (_kundeServiceVM.SelectedUser == null)
            {
                // Handle the case where no user is selected
                return;
            }

            try
            {
                // Create a new message model with the file name
                var message = new MessageModel(Content, _kundeServiceVM.Admin, _kundeServiceVM.SelectedUser.UserId.ToString(), FileName);

                // Save the message in Firestore using the FirebaseStore instance
                await _firebaseStore.SendMessageAsync(message);

                // Optionally, you can add the saved message to a local collection for display
                // messages.Add(message);

                // Clear the content and file name after sending the message
                Content = "";
                FileName = "";

                // Optionally, you can notify observers that a message has been sent
                // OnPropertyChanged(nameof(messages));
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during message sending
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
                FileName = Path.GetFileName(openFileDialog.FileName); // Store only the file name

                // Open the file stream
                using (var stream = openFileDialog.OpenFile())
                {
                    // Upload the file to Firebase Storage
                    await _firebaseStore.UploadReportAsync(stream, FileName);

                    // Add the file to the Firestore database
                    await _firebaseStore.AddReportAsync(FileName);
                }

                // Clear the items collection
                Items.Clear();
            }
        }
    }
}
