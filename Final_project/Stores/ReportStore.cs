using Domain.Models;
using Report_Generator_Domain.Commands;

using Report_Generator_Domain.Models;
using Report_Generator_Domain.Queries;

namespace Final_project.Stores
{
    public class ReportStore

    {

        public IEnumerable<ReportModel> ReportModels => _reportmodel;
        public IEnumerable<ReportImageModel> ReportImageModels => _reportImagemodel;

        private readonly IGetAllReportsQuery _query;
        private readonly ICreateReportCommand _createReportCommand;
        private readonly IDeleteReportCommand _deleteReportCommand;
        private readonly IUpdateReportCommand _updateReportCommand;
        private readonly IGetReportDataCommand _getReportDataCommand;




        private readonly List<ReportModel> _reportmodel;
        private readonly List<ReportImageModel> _reportImagemodel;

        public ReportStore(IGetAllReportsQuery query,
             ICreateReportCommand createReportCommand,
             IDeleteReportCommand deleteReportCommand,
             IUpdateReportCommand updateReportCommand,
             IGetReportDataCommand getReportDataCommand

            )
        {
            _query = query;
            _createReportCommand = createReportCommand;
            _deleteReportCommand = deleteReportCommand;
            _updateReportCommand = updateReportCommand;
            _getReportDataCommand = getReportDataCommand;





            _reportmodel = new List<ReportModel>();
            _reportImagemodel = new List<ReportImageModel>();
        }

        public event Action ReportModelLoaded;
        public event Action<ReportModel> ReportAdded;
        public event Action<ReportModel> ReportUpdated;
        public event Action<Guid> ReportDeleted;

        public event Action<ReportImageModel> ImageAdded;
        public event Action<Guid> ImageDeleted;



        public async Task Load()
        {
            IEnumerable<ReportModel> reportModels = await _query.Execute();

            _reportmodel.Clear();
            _reportmodel.AddRange(reportModels);

            ReportModelLoaded?.Invoke();
        }

        public async Task Add(ReportModel reportModel)
        {
            await _createReportCommand.Execute(reportModel);

            _reportmodel.Add(reportModel);
            ReportAdded?.Invoke(reportModel);
        }


        public async Task Update(ReportModel reportModel)
        {

            await _updateReportCommand.Execute(reportModel);

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



        public async Task<(
             ReportModel report, DataFraOppdragsgiverPrøverModel DataFraOppdragsgiverPrøverModel,
            List<DataFraOppdragsgiverPrøverModel> dataFraOppdragsgiverPrøverModels,
            List<ReportImageModel> images,
            List<DataEtterKuttingOgSlipingModel> dataEtterKuttingOgSlipingModels,
            List<ConcreteDensityModel> concreteDensityModels,
            List<TrykktestingModel> trykktestingModels,
            List<TestModel> tests,
            List<verktøyModel> verktøyer
            )> GetReportData(Guid reportId)
        {
            return await _getReportDataCommand.Execute(reportId);
        }









    }




}


