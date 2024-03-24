namespace Final_project.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigation;

        public ViewModelBase CurrentViewModel => _navigation.CurrentViewModel;

        public MainViewModel(NavigationStore navigation)
        {
            _navigation = navigation;

            _navigation.CurrentViewModelChanged += GetOnCurrentViewModelChanged;
        }

        private void GetOnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
