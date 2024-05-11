
using System.Windows;
using System.Windows.Controls;

namespace Final_project.ViewModels
{
    public class CustomModalVM : ContentControl
    {
        static CustomModalVM()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomModalVM), new FrameworkPropertyMetadata(typeof(CustomModalVM)));
            IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(CustomModalVM), new PropertyMetadata(false));
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // DependencyProperty for IsOpen
        public static readonly DependencyProperty IsOpenProperty;

        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(CustomModalVM), new PropertyMetadata(true));

        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }
    }
}
