namespace Domain.Models
{
    public class ReportModel
    {

        public Guid Id { get; }
        public string Tittle { get; }
        public bool Status { get; }
        public string Kunde { get; }

        public ReportModel(Guid id, string tittle, bool status, string kunde)
        {
            Id = id;
            Tittle = tittle;
            Status = status;
            Kunde = kunde;
        }



    }
}
