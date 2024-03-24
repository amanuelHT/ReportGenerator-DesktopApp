using Final_project.ViewModels;

public class NavigationStore
{
    public event Action CurrentViewModelChanged;
    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();
            _currentViewModel = value;
            OnCurrentViewModelChanged();


        }
    }



    protected void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}
