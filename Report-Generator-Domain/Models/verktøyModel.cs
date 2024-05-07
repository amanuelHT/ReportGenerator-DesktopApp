using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class verktøyModel
    {
        public Guid Id { get; set; }

        public ReportModel ReportModel { get; set; }
        public Guid ReportModelId { get; set; }

        public string Name { get; set; }
        public verktøyModel(Guid id, string name, Guid reportModelId)
        {
            Id = id;
            Name = name;
            ReportModelId = reportModelId;
        }
    }
}
