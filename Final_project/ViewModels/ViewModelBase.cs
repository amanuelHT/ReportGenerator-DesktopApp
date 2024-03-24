using System.ComponentModel;

namespace Final_project.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void Dispose()
        {


        }


    }
}

