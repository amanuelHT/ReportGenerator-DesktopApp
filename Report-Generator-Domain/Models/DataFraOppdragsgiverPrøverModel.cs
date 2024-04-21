using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class DataFraOppdragsgiverPrøverModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ReportModelId { get; set; } // Foreign key
        public ReportModel ReportModel { get; set; } // Navigation property

        // Adjusted constructor to initialize TableModelId
        public DataFraOppdragsgiverPrøverModel(Guid id, string name, string description, Guid reportModelId)
        {
            Id = id;
            Name = name;
            Description = description;
            ReportModelId = reportModelId; // Initialize TableModelId
        }
    }
}
