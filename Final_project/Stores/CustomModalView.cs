using System.Windows;
using System.Windows.Controls;

namespace Final_project.Stores
{
    public class CustomModalView : ContentControl
    {
        static CustomModalView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomModalView), new FrameworkPropertyMetadata(typeof(CustomModalView)));
            IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(CustomModalView), new PropertyMetadata(false));
        }

        // Define the IsOpen property
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // DependencyProperty for IsOpen
        public static readonly DependencyProperty IsOpenProperty;

        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(CustomModalView), new PropertyMetadata(true));

        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }
    }
}
