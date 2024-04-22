using System.Diagnostics;
using System.Windows;

namespace Final_project.Views
{
    public partial class ModalWindow : Window
    {
        public ModalWindow()
        {
            InitializeComponent();
        }

        public void CloseWindow()
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Debug.WriteLine($"Exception occurred while closing the window: {ex.Message}");
            }
        }

    }
}
