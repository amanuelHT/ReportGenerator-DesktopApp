using CommunityToolkit.Mvvm.ComponentModel;

namespace Final_project.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly NavigationStore _navigation;

        public ObservableObject CurrentViewModel => _navigation.CurrentViewModel;

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
