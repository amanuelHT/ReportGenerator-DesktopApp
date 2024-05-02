using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Report_Generator_Domain.Models;

namespace Final_project.ViewModels
{
    public partial class MessageVM : ObservableObject
    {

        // Instance of KundeServiceVM to access the selected user
        private KundeServiceVM _kundeServiceVM;

        public MessageVM(KundeServiceVM kundeServiceVM)
        {
            _kundeServiceVM = kundeServiceVM ?? throw new ArgumentNullException(nameof(kundeServiceVM));
        }

        [ObservableProperty]
        public string _content;

        [ObservableProperty]
        public UserInfo _sender;


        [RelayCommand]
        private void SendMessage()
        {
            // Ensure a user is selected
            if (_kundeServiceVM.SelectedUser == null)
            {
                // Handle the case where no user is selected
                return;
            }

            try
            {
                // Create a new instance of MessageModel with the necessary parameters
                var message = new MessageModel(Content, Sender, _kundeServiceVM.SelectedUser);

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
    }
}
