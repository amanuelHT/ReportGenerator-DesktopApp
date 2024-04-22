using CommunityToolkit.Mvvm.ComponentModel;

public class NavigationStore
{
    public event Action CurrentViewModelChanged;
    private ObservableObject _currentViewModel;

    public ObservableObject CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    protected void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}
