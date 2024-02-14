using Final_project.Stores;

namespace Final_project.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentVM => _navigationStore.CurrentView;
        public bool IsFormOpen => _navigationStore.IsOpen;




        public HomeVM HomeVM { get; }


        public MainViewModel(NavigationStore navigationStore, HomeVM homeVM)
        {
            _navigationStore = navigationStore;
            HomeVM = homeVM;

            _navigationStore.CurrentViewChanged += NavigationStore_CurrentViewChanged;


        }

        protected override void Dispose()
        {
            _navigationStore.CurrentViewChanged -= NavigationStore_CurrentViewChanged;

            base.Dispose();
        }

        private void NavigationStore_CurrentViewChanged()
        {
            OnPropertyChanged(nameof(CurrentVM));
            OnPropertyChanged(nameof(IsFormOpen));

        }

    }
}
