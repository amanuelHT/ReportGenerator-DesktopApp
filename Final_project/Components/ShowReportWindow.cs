using Final_project.Views;

namespace Final_project.Components
{
    public static class ReportWindowHelper
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
