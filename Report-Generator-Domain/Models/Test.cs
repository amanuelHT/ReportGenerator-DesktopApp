using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class Test
    {

        public ReportModel ReportModel { get; set; }
        public Guid ReportModelId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public Test(Guid reportModelId, int id, string name)
        {
            ReportModelId = reportModelId;
            Id = id;
            Name = name;
        }
    }
}
