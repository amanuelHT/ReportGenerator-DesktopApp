using Final_project.Models;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public class ReportListingItemVM : ViewModelBase
    {
        public ReportModel ReportModel { get; }

        public string Tittle => ReportModel.Tittle;
        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; }



        public ReportListingItemVM(ReportModel reportModel)
        {
            ReportModel = reportModel;
        }
    }
}
