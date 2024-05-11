using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Views;

namespace Final_project.ViewModels
{
    public class ModalWindowVM : ObservableRecipient
    {
        public static void ShowReportWindow(object viewModel, string title)
        {
            ModalWindow modalWindow = new ModalWindow
            {
                Title = title,
                DataContext = viewModel
            };

            modalWindow.ShowDialog();
        }


    }
}
