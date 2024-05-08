using Report_Generator_Domain.Models;

namespace Domain.Models
{
    public class ReportModel
    {

        public Guid Id { get; set; }


        public string Tittle { get; set; }
        public bool Status { get; set; }
        public string Kunde { get; set; }



        public string AvvikFraStandarder { get; set; }
        public DateTime MotattDato { get; set; }
        public string Kommentarer { get; set; }
        public int UiaRegnr { get; set; }




        // Collection of ReportImageModel
        public List<ReportImageModel> Images { get; set; }
        public List<TestModel> Test { get; set; }
        public List<verktøyModel> Verktøy { get; set; }
        public List<DataFraOppdragsgiverPrøverModel> DataFraOppdragsgiverPrøver { get; set; }

        public List<DataEtterKuttingOgSlipingModel> DataEtterKuttingOgSlipingModel { get; set; }


        public List<ConcreteDensityModel> ConcreteDensityModel { get; set; }

        public List<TrykktestingModel> TrykktestingModel { get; set; }


        public TestUtførtAvModel TestUtførtAvModel { get; set; }
        public KontrollertAvførtAvModel KontrollertAvførtAvModel { get; set; }



        public ReportModel(Guid id, string tittle, bool status, string kunde, string avvikFraStandarder, DateTime motattDato, string kommentarer, int uiaRegnr)
        {
            Id = id;
            Tittle = tittle;
            Status = status;
            Kunde = kunde;
            AvvikFraStandarder = avvikFraStandarder;
            MotattDato = motattDato;
            Kommentarer = kommentarer;
            UiaRegnr = uiaRegnr;



            Images = new List<ReportImageModel>();
            Test = new List<TestModel>();
            Verktøy = new List<verktøyModel>();
            DataFraOppdragsgiverPrøver = new List<DataFraOppdragsgiverPrøverModel>();
            DataEtterKuttingOgSlipingModel = new List<DataEtterKuttingOgSlipingModel>();
            ConcreteDensityModel = new List<ConcreteDensityModel>();
            TrykktestingModel = new List<TrykktestingModel>();
            TestUtførtAvModel = new TestUtførtAvModel();
            KontrollertAvførtAvModel = new KontrollertAvførtAvModel();


        }



    }
}
