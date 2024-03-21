namespace Final_project.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public HomeVM HomeVM { get; }


        public ViewModelBase CurrentView { get; }

        public MainViewModel(HomeVM homeVM)
        {
            CurrentView = homeVM;

            //HomeVM = homeVM;
        }
    }
}


