using CommunityToolkit.Mvvm.ComponentModel;

namespace Final_project.ViewModels
{
    public class GeneratedReportVM : ObservableObject
    {
        public string Name { get; }

        public GeneratedReportVM(string name)
        {
            Name = name;
        }

    }
}
