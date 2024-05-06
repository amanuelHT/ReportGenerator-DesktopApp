using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class TestUtførtAvModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }


        public int ReportModelID { get; set; }
        public ReportModel Report { get; set; }

        public TestUtførtAvModel(int iD, string name, string department, DateTime date, string title, int reportModelID)
        {
            ID = iD;
            Name = name;
            Department = department;
            Date = date;
            Title = title;
            ReportModelID = reportModelID;
        }
    }
}
