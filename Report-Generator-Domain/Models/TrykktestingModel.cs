

using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class TrykktestingModel
    {
        public Guid Id { get; set; }

        public int Prøvenr { get; set; }
        public decimal TrykkflateMm { get; set; }
        public decimal PalastHastighetMPas { get; set; }
        public decimal TrykkfasthetMPa { get; set; }
        public decimal TrykkfasthetMPaNSE { get; set; }
        public Guid ReportModelId { get; set; }
        public ReportModel ReportModel { get; set; }

        public TrykktestingModel(Guid id, int prøvenr, decimal trykkflateMm, decimal palastHastighetMPas,
                                         decimal trykkfasthetMPa, decimal trykkfasthetMPaNSE, Guid reportModelId)
        {
            Id = id;
            Prøvenr = prøvenr;
            TrykkflateMm = trykkflateMm;
            PalastHastighetMPas = palastHastighetMPas;
            TrykkfasthetMPa = trykkfasthetMPa;
            TrykkfasthetMPaNSE = trykkfasthetMPaNSE;
            ReportModelId = reportModelId;

        }
    }
}