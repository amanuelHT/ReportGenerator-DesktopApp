namespace Final_project.Models
{
    public class ReportModel
    {

        public string Tittle { get; set; }
        public bool Status { get; set; }
        public string Kunde { get; set; }

        public ReportModel(string tittle, bool status, string kunde)
        {

            Tittle = tittle;
            Status = status;
            Kunde = kunde;
        }



    }
}
