namespace Final_project.Models
{
    public class ReportModel
    {

        public Guid Id { get; set; }
        public string Tittle { get; set; }
        public bool Status { get; set; }
        public string Kunde { get; set; }

        public ReportModel(Guid id, string tittle, bool status, string kunde)
        {
            Id = id;
            Tittle = tittle;
            Status = status;
            Kunde = kunde;
        }



    }
}
