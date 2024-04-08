// Modify the ReportModel class
namespace Domain.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public string Tittle { get; set; } // Note: Corrected the spelling of 'Title'
        public bool Status { get; set; }
        public string Kunde { get; set; }

        // Collection of ReportImageModel
        public List<ReportImageModel> Images { get; set; }

        public ReportModel(Guid id, string tittle, bool status, string kunde)
        {
            Id = id;
            Tittle = tittle;
            Status = status;
            Kunde = kunde;
            Images = new List<ReportImageModel>();
        }


    }
}
