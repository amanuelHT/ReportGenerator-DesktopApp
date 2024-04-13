using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Stores;
using System.Windows.Input;

namespace Final_project.ViewModels
{
    public partial class ReportFormVM : ObservableObject
    {


        public List<ReportImageModel> Images { get; set; }

        public ImageCollectionVM ImageCollectionViewModel { get; set; }



        [ObservableProperty]
        private string _tittle;

        [ObservableProperty]
        private bool _status;


        [ObservableProperty]
        private string _kunde;



        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ReportFormVM(ICommand submitCommand, ICommand cancelCommand, ReportStore reportStore, Guid reportid)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;

            ImageCollectionViewModel = new ImageCollectionVM(reportStore, reportid);

        }



    }
}
