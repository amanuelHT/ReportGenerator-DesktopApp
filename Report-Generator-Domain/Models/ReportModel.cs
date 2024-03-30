namespace Domain.Models
{
    public class ReportModel
    {
        public Guid Id { get; }
        public string Tittle { get; } // Note: Corrected the spelling of 'Title'
        public bool Status { get; }
        public string Kunde { get; }

        // Collection of ReportImageModel
        public List<ReportImageModel> Images { get; private set; }

        public ReportModel(Guid id, string tittle, bool status, string kunde)
        {
            Id = id;
            Tittle = tittle; // Corrected spelling here too
            Status = status;
            Kunde = kunde;
            Images = new List<ReportImageModel>();
        }


    }
}

