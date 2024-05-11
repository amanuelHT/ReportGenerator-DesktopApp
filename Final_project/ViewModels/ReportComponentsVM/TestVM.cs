using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Report_Generator_Domain.Models;


namespace Final_project.ViewModels.ReportComponentsVM
{
    public partial class TestVM : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        private readonly TestCollectionVM _testCollectionVM;
        private readonly Guid _reportModelId;


        public TestVM(Guid reportid, TestCollectionVM testCollectionVM)
        {

            _reportModelId = reportid;
            _testCollectionVM = testCollectionVM;

        }

        public TestVM(TestModel model)
        {
            if (model != null)
            {

                Name = model.Name;
                _reportModelId = model.ReportModelId;

            }
        }

        [RelayCommand]
        public void Submit()
        {

            var newEntry = new TestModel(
               Guid.NewGuid(),
                Name,
               _reportModelId
            );



            var newtest = new TestVM(newEntry)
            {
            };


            _testCollectionVM.tests.Add(newtest);

            Name = "";

        }

    }
}
