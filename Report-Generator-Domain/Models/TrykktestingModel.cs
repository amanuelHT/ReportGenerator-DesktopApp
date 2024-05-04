namespace Report_Generator_Domain.Models
{
    public class TrykktestingModel
    {
        public Guid Id { get; set; }
        public decimal TrykkflateMm { get; set; }
        public decimal PalastHastighetMPas { get; set; }
        public decimal TrykkfasthetMPa { get; set; }
        public decimal TrykkfasthetMPaNSE { get; set; }
        public Guid DataFraOpdragsgiverId { get; set; }

        public DataFraOppdragsgiverPrøverModel DataFraOppdragsgiverPrøverModel { get; set; }
        public TrykktestingModel(Guid id, decimal trykkflateMm, decimal palastHastighetMPas, decimal trykkfasthetMPa, decimal trykkfasthetMPaNSE, Guid dataFraOpdragsgiverId)
        {
            Id = id;
            TrykkflateMm = trykkflateMm;
            PalastHastighetMPas = palastHastighetMPas;
            TrykkfasthetMPa = trykkfasthetMPa;
            TrykkfasthetMPaNSE = trykkfasthetMPaNSE;
            DataFraOpdragsgiverId = dataFraOpdragsgiverId;

        }
    }
}
