//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using System.Windows.Input;

//namespace Final_project.ViewModels
//{
//    public class ModalViewModel : ObservableRecipient
//    {
//        private string _modalMessage;
//        public string ModalMessage
//        {
//            get { return _modalMessage; }
//            set { SetProperty(ref _modalMessage, value); }
//        }

//        private ICommand _closeCommand;
//        public ICommand CloseCommand
//        {
//            get
//            {
//                if (_closeCommand == null)
//                {
//                    _closeCommand = new RelayCommand(() =>
//                    {
//                        // Close the modal window here
//                        // For example, you might set a property to indicate the window should close
//                        // or call a method on the window itself to close it
//                    });
//                }
//                return _closeCommand;
//            }
//        }

//        public ModalViewModel()
//        {
//            // Initialize any properties or commands here
//            ModalMessage = "Hello, Modal!";
//        }
//    }
//}
