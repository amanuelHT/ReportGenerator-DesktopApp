using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Final_project.Components
{

    public partial class ReportForm : System.Windows.Controls.UserControl
    {
        public ReportForm()
        {
            InitializeComponent();
        }
        private void ChildControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            var eventArgs = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = UIElement.MouseWheelEvent,
                Source = sender
            };
            ((UIElement)sender).RaiseEvent(eventArgs);
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {

                if (e.Delta < 0)
                {
                    scrollViewer.LineDown();
                }
                else
                {
                    scrollViewer.LineUp();
                }

                e.Handled = true;
            }
        }

    }
}