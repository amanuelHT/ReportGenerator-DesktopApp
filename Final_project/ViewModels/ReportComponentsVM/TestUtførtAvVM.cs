using CommunityToolkit.Mvvm.ComponentModel;
using Report_Generator_Domain.Models;

namespace Final_project.ViewModels.ReportComponentsVM
{
    public partial class TestUtførtAvVM : ObservableObject
    {
        private readonly Guid _reportModelid;
        private readonly Guid _testUtførtAvVMID;

        private readonly ReportFormVM _reportFormVM;
        TestUtførtAvVM testUtførtAvVMData { get; set; }

        public TestUtførtAvVM(Guid reportModelid, ReportFormVM reportFormVM)
        {
            _reportModelid = reportModelid;
            reportFormVM = reportFormVM;
        }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _position;

        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private string _department;

        public TestUtførtAvVM(TestUtførtAvModel model)
        {
            if (model != null)
            {

                _testUtførtAvVMID = model.ID;
                Name = model.Name;
                Department = model.Department;
                Date = model.Date;
                Position = model.Position;
                _reportModelid = model.ID;
            }
        }
    }
}
