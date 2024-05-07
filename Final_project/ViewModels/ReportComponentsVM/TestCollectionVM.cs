using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Final_project.ViewModels.ReportComponentsVM
{
    public partial class TestCollectionVM : ObservableObject
    {

        private readonly TestVM testVM;
        public ObservableCollection<TestVM> tests { get; }


        public Guid _reportid { get; }

        public TestCollectionVM(Guid reportid)
        {



            tests = new ObservableCollection<TestVM>();
            _reportid = reportid;

        }


        [RelayCommand]
        public void AddRecord()
        {

            var testvm = new TestVM(_reportid, this);



        }



        [RelayCommand]
        public void RemovePrøve(TestVM testVM)
        {
            if (testVM != null && tests.Contains(testVM))
            {
                tests.Remove(testVM);
            }
        }
    }
}
