using CommunityToolkit.Mvvm.ComponentModel;




namespace Final_project.ViewModels

{
    public class LayoutVM : ObservableObject

    {
        public NavigationBarVM NavigationBarVM { get; }
        public ObservableObject ContentViewModel { get; }

        public LayoutVM(NavigationBarVM navigationBarViewModel, ObservableObject contentViewModel)
        {
            NavigationBarVM = navigationBarViewModel;
            ContentViewModel = contentViewModel;
        }


    }
}