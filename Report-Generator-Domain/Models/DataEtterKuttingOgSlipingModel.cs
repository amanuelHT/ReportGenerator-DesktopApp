


using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class DataEtterKuttingOgSlipingModel
    {

        public Guid Id { get; set; }
        public int Prøvenr { get; set; }
        public DateTime IvannbadDato { get; set; }
        public DateTime TestDato { get; set; }
        public string Overflatetilstand { get; set; }
        public double Dm { get; set; }
        public double Prøvetykke { get; set; }
        public double DmPrøvetykkeRatio { get; set; }
        public double TrykkfasthetMPa { get; set; }
        public string FasthetSammenligning { get; set; }
        public double FørSliping { get; set; }
        public double EtterSliping { get; set; }
        public double MmTilTopp { get; set; }

        public Guid ReportModelId { get; set; } // Foreign key
        public ReportModel ReportModel { get; set; }


        public DataEtterKuttingOgSlipingModel(Guid id, int prøvenr, DateTime ivannbadDato, DateTime testDato, string overflatetilstand,
                                                                         double dm, double prøvetykke, double dmPrøvetykkeRatio,
                                                                             double trykkfasthetMPa, string fasthetSammenligning,
                                                                              double førSliping, double etterSliping, double mmTilTopp, Guid reportModelId)
        {
            Id = id;
            Prøvenr = prøvenr;
            IvannbadDato = ivannbadDato;
            TestDato = testDato;
            Overflatetilstand = overflatetilstand;
            Dm = dm;
            Prøvetykke = prøvetykke;
            DmPrøvetykkeRatio = dmPrøvetykkeRatio;
            TrykkfasthetMPa = trykkfasthetMPa;
            FasthetSammenligning = fasthetSammenligning;
            FørSliping = førSliping;
            EtterSliping = etterSliping;
            MmTilTopp = mmTilTopp;
            ReportModelId = reportModelId;
        }
    }
}