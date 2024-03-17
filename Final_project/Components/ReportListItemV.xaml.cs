using System.Windows;

namespace Final_project.Components
{
    /// <summary>
    /// Interaction logic for ReportListItemV.xaml
    /// </summary>
    public partial class ReportListItemV : System.Windows.Controls.UserControl
    {
        public ReportListItemV()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dropdown.IsOpen = false;

        }
    }
}
