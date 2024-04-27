using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
namespace Final_project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParm, int lparm);

        private void panelcontrol_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void panelcontrol_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to close the window here (e.g., using Window.Close())
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to minimize the window here (e.g., using Window.WindowState)
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }

        private void maximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to maximize/normalize the window here (e.g., using Window.WindowState)
            Window window = Window.GetWindow(this);
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }





    }

}

