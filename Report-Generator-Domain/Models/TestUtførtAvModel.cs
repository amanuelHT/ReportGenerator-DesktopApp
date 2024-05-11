using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class TestUtførtAvModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime Date { get; set; }
        public string Position { get; set; }
        public Guid ReportModelID { get; set; }
        public ReportModel Report { get; set; }

        public TestUtførtAvModel()
        {
            
        }

        public TestUtførtAvModel(Guid id, string name, string department, DateTime date, string position, Guid reportModelID)
        {
            ID = id;
            Name = name;
            Department = department;
            Date = date;
            Position = position;
            ReportModelID = reportModelID;
        }
    }
}
