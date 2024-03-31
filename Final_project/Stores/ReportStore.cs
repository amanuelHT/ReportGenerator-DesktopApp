using Domain.Models;
using Report_Generator_Domain.Commands;
using Report_Generator_Domain.Queries;

namespace Final_project.Stores
{
    public class ReportStore

    {

        public IEnumerable<ReportModel> ReportModels => _reportmodel;

        private readonly IGetAllReportsQuery _query;
        private readonly ICreateReportCommand _createReportCommand;
        private readonly IDeleteReportCommand _deleteReportCommand;
        private readonly IUpdateReportCommand _updateReportCommand;
        private readonly IGetReportDataCommand _getReportDataCommand;
        private readonly IGetReportImageCommand _getReportImageCommand;



        private readonly List<ReportModel> _reportmodel;

        public ReportStore(IGetAllReportsQuery query,
            ICreateReportCommand createReportCommand,
            IDeleteReportCommand deleteReportCommand,
            IUpdateReportCommand updateReportCommand,
             IGetReportDataCommand getReportDataCommand,
             IGetReportImageCommand getReportImageCommand
            )
        {
            _query = query;
            _createReportCommand = createReportCommand;
            _deleteReportCommand = deleteReportCommand;
            _updateReportCommand = updateReportCommand;
            _getReportDataCommand = getReportDataCommand;
            _getReportImageCommand = getReportImageCommand;




            _reportmodel = new List<ReportModel>();
        }

        public event Action ReportModelLoaded;
        public event Action<ReportModel> ReportAdded;
        public event Action<ReportModel> ReportUpdated;
        public event Action<Guid> ReportDeleted;


        public async Task Load()
        {
            IEnumerable<ReportModel> reportModels = await _query.Execute();

            _reportmodel.Clear();
            _reportmodel.AddRange(reportModels);
            ReportModelLoaded?.Invoke();
        }

        public async Task Add(ReportModel reportModel, List<ReportImageModel> images)
        {
            await _createReportCommand.Execute(reportModel, images);

            _reportmodel.Add(reportModel);
            ReportAdded?.Invoke(reportModel);
        }



        public async Task Update(ReportModel reportModel, List<ReportImageModel> images)
        {


            await _updateReportCommand.Execute(reportModel, images);

            int currentIndex = _reportmodel.FindIndex(y => y.Id == reportModel.Id);
            if (currentIndex == -1)
            {
                _reportmodel[currentIndex] = reportModel;
            }
            else
            {
                _reportmodel.Add(reportModel);
            }

            ReportUpdated?.Invoke(reportModel);
        }
        public async Task Delete(Guid id)
        {
            await _deleteReportCommand.Execute(id);

            _reportmodel.RemoveAll(y => y.Id == id);

            ReportDeleted?.Invoke(id);
        }


        public async Task<ReportModel> GetReportData(Guid reportId)
        {
            return await _getReportDataCommand.Execute(reportId);
        }


        public async Task<ReportModel> GetImages(Guid reportId)
        {
            // Get the report details
            var reportModel = await _getReportDataCommand.Execute(reportId);

            // If no report is found, return null
            if (reportModel == null)
            {
                return null;
            }

            // Retrieve the associated images
            var reportImages = await _getReportImageCommand.Execute(reportId);

            // If there are images, add them to the report's images collection
            reportModel.Images.AddRange(reportImages);

            // Return the report with the images
            return reportModel;
        }



        // In your ReportStore
        public async Task<ReportModel> GetReportDataWithImages(Guid reportId)
        {
            // Get the report details and associated images using the new method name
            var reportModel = await GetImages(reportId);
            return reportModel;
        }

    }




}


