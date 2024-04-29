using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.Views;

namespace Final_project.Stores
{
    public class ModalNavigation
    {
        private ObservableObject _currentView;
        public ObservableObject CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    CurrentViewChanged?.Invoke();
                }
            }
        }

        public event Action CurrentViewChanged;

        public bool IsOpen => CurrentView != null;

        public void ShowReportWindow(object viewModel, string title)
        {

            ModalWindow modalWindow = new ModalWindow
            {
                Title = title,
                DataContext = viewModel
            };


            modalWindow.ShowDialog();
        }

        public void Close()
        {
            CurrentView = null;
        }


    }
}
