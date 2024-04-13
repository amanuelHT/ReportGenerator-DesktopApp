using CommunityToolkit.Mvvm.ComponentModel;

namespace Final_project.Service
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ObservableObject
    {
        private readonly NavigationStore _navigation;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigation, Func<TViewModel> createViewModel)
        {
            _navigation = navigation;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigation.CurrentViewModel = _createViewModel();
        }





    }

}


