using Final_project.Models;

namespace Final_project.Stores
{
    public class ReportStore

    {



        public event Action<ReportModel> ReportAdded;
        public event Action<ReportModel> ReportUpdated;


        public async Task Add(ReportModel reportModel)
        {

            ReportAdded?.Invoke(reportModel);
        }




        public async Task Update(ReportModel reportModel)
        {

            ReportUpdated?.Invoke(reportModel);
        }




    }

}
