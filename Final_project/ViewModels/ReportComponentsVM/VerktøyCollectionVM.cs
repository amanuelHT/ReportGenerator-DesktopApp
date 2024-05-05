using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels.ReportComponentsVM
{
    public partial class VerktøyCollectionVM : ObservableObject
    {
        private readonly TestVM testVM;
        public ObservableCollection<VerktøyVM> verktøyVMs { get; }


        public Guid _reportid { get; }

        public VerktøyCollectionVM(Guid reportid)
        {



            verktøyVMs = new ObservableCollection<VerktøyVM>();
            _reportid = reportid;

        }


        [RelayCommand]
        public void AddRecord()
        {

            var verktøy = new VerktøyVM(this, _reportid);



        }



        [RelayCommand]
        public void RemovePrøve(VerktøyVM verktøy)
        {
            if (verktøy != null && verktøyVMs.Contains(verktøy))
            {
                verktøyVMs.Remove(verktøy);
            }
        }
    }
}
