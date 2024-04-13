using CommunityToolkit.Mvvm.ComponentModel;

namespace Final_project.Stores
{
    public class ModalNavigation
    {
        public ObservableObject _currentView;
        public ObservableObject CurrentView
        {
            get { return _currentView; }
            set
            {


                _currentView = value;
                CurrentViewChanged?.Invoke();

            }
        }



        public bool IsOpen => CurrentView != null;

        public event Action CurrentViewChanged;

        internal void Close()
        {
            CurrentView = null;
        }
    }
}