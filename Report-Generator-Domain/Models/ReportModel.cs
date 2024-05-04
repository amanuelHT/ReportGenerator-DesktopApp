using Report_Generator_Domain.Models;

namespace Domain.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }


        public string Tittle { get; set; } // Corrected spelling to 'Title'
        public bool Status { get; set; }
        public string Kunde { get; set; }

        // Collection of ReportImageModel
        public List<ReportImageModel> Images { get; set; }
        public List<Test> Test { get; set; }
        public List<verktøy> Verktøy { get; set; }
        public List<DataFraOppdragsgiverPrøverModel> DataFraOppdragsgiverPrøver { get; set; }




        public ReportModel(Guid id, string tittle, bool status, string kunde)
        {
            Id = id;
            Tittle = tittle;
            Status = status;
            Kunde = kunde;

            Images = new List<ReportImageModel>();
            Test = new List<Test>();
            Verktøy = new List<verktøy>();
            DataFraOppdragsgiverPrøver = new List<DataFraOppdragsgiverPrøverModel>();


        }
    }
}
