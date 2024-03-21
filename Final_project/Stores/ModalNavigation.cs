using Final_project.ViewModels;

namespace Final_project.Stores
{
    public class ModalNavigation
    {
        public ViewModelBase _currentView;
        public ViewModelBase CurrentView
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
