namespace Final_project.ViewModels
{
    public class LayoutVM : ViewModelBase
    {
        public NavigationBarVM NavigationBarVM { get; }
        public ViewModelBase ContentViewModel { get; }

        public LayoutVM(NavigationBarVM navigationBarViewModel, ViewModelBase contentViewModel)
        {
            NavigationBarVM = navigationBarViewModel;
            ContentViewModel = contentViewModel;
        }

        public override void Dispose()
        {
            NavigationBarVM.Dispose();
            ContentViewModel.Dispose();

            base.Dispose();
        }
    }
}