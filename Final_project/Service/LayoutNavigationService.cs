using CommunityToolkit.Mvvm.ComponentModel;
using Final_project.ViewModels;

namespace Final_project.Service
{
    public class LayoutNavigationService<TViewModel> : INavigationService where TViewModel : ObservableObject
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Func<NavigationBarVM> _createNavigationBarViewModel;

        public LayoutNavigationService(NavigationStore navigationStore,
            Func<TViewModel> createViewModel,
            Func<NavigationBarVM> createNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createNavigationBarViewModel = createNavigationBarViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new LayoutVM(_createNavigationBarViewModel(), _createViewModel());
        }
    }
}
