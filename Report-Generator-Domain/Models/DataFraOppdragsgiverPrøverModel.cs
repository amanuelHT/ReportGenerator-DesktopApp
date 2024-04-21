using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class DataFraOppdragsgiverPrøverModel
    {

        public Guid Id { get; set; }

        public DateTime Datomottatt { get; set; }
        public string Overdekningoppgitt { get; set; }
        public string Dmax { get; set; }
        public int KjerneImax { get; set; }
        public int KjerneImin { get; set; }
        public string OverflateOK { get; set; }
        public string OverflateUK { get; set; }
        public Guid ReportModelId { get; set; } // Foreign key
        public ReportModel ReportModel { get; set; } // Navigation property

        // Adjusted constructor to initialize TableModelId
        public DataFraOppdragsgiverPrøverModel(Guid id, DateTime datomottatt, string overdekningoppgitt, string dmax, int kjerneImax, int kjerneImin, string overflateOK, string overflateUK, Guid reportModelId)
        {
            Id = id;
            Datomottatt = datomottatt;
            Overdekningoppgitt = overdekningoppgitt;
            Dmax = dmax;
            KjerneImax = kjerneImax;
            KjerneImin = kjerneImin;
            OverflateOK = overflateOK;
            OverflateUK = overflateUK;
            ReportModelId = reportModelId;
        }

    }
}
