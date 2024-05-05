using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Report_Generator_Domain.Models;

namespace Final_project.ViewModels.ReportComponentsVM
{
    public partial class VerktøyVM : ObservableObject
    {
        [ObservableProperty]
        private string _name;


        private readonly VerktøyCollectionVM _verktøyCollectionVM;
        private readonly Guid _reportModelId;



        public VerktøyVM(VerktøyCollectionVM verktøyCollectionVM, Guid reportModelId)
        {
            _verktøyCollectionVM = verktøyCollectionVM;
            _reportModelId = reportModelId;
        }







        // Constructor to populate from an existing model
        public VerktøyVM(verktøyModel model)
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
            var newEntry = new verktøyModel(

                 Guid.NewGuid(),
                  Name,
                _reportModelId

            );



            var newtest = new VerktøyVM(newEntry)
            {
            };


            _verktøyCollectionVM.verktøyVMs.Add(newtest);

        }
    }
}
