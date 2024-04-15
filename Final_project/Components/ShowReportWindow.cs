using System.Windows;

namespace Final_project.Components
{
    public static class ReportWindowHelper
    {
        public static void ShowReportWindow(object viewModel, string title)
        {
            // Create a new window for editing or adding the report
            Window reportWindow = new Window
            {
                Title = title,
                Width = 600,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = viewModel
            };

            // Show the window
            reportWindow.ShowDialog();
        }
    }
}
