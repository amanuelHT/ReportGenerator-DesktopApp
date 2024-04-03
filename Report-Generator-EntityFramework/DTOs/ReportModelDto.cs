namespace Report_Generator_EntityFramework.DTOs
{
    public class ReportModelDto
    {


        public Guid Id { get; set; }
        public string Tittle { get; set; }
        public bool Status { get; set; }
        public string Kunde { get; set; }


        public virtual List<ReportImageModelDto> Images { get; set; }
    }
}
