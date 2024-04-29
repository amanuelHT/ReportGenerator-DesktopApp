using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Views;

namespace Final_project.ViewModels
{
    public class ModalWindowVM : ObservableRecipient
    {
        public static void ShowReportWindow(object viewModel, string title)
        {
            // Create a new instance of the ModalWindow
            ModalWindow modalWindow = new ModalWindow
            {
                Title = title,
                DataContext = viewModel
            };

            // Show the modal window
            modalWindow.ShowDialog();
        }


    }
}
