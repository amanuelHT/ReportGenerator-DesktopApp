using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;
using Final_project.Stores;

namespace Final_project.ViewModels.ReportComponentsVM
{
    public partial class ImageVM : ObservableObject
    {
        private readonly ImageCollectionVM _imageCollectionVM;
        private readonly ReportStore _reportStore;
        private readonly Guid _reportId;

        public ReportImageModel ReportImageModel { get; private set; }

        private Uri _imageUri;

        public Guid ImageId { get; set; }
        public string ImageName { get; set; }



        public Uri ImageUri
        {
            get => _imageUri;
            set => SetProperty(ref _imageUri, value);
        }

        public ImageVM(Guid reportid, ImageCollectionVM imageCollectionVM, ReportStore reportStore)
        {
            _reportId = reportid;
            _imageCollectionVM = imageCollectionVM;
            _reportStore = reportStore;


        }

        public ImageVM(ReportImageModel reportImageModel)
        {
            if (reportImageModel != null)
            {
                ImageId = reportImageModel.Id;
                ImageName = reportImageModel.Name;
                ImageUri = new Uri(reportImageModel.ImageUrl, UriKind.RelativeOrAbsolute);
            }
        }

        public void Submit()
        {
            var newImage = new ReportImageModel(
                ImageId == Guid.Empty ? Guid.NewGuid() : ImageId,
                ImageName,
                ImageUri.ToString(),
                _reportId
            );

            var newPrøveVM = new ImageVM(newImage);

            if (ImageId == Guid.Empty)
            {
                _imageCollectionVM.Images.Add(newPrøveVM);
            }
        }




    }
}
