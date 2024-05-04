namespace Report_Generator_Domain.Models
{
    public class ConcreteDensityModel
    {
        public int Id { get; set; }  // Assuming Provnr is an integer (int)
        public DateTime Dato { get; set; }  // Assuming Dato is a date (DateTime)
        public double MasseILuft { get; set; }  // Assuming Masse i luft is a decimal (double)
        public double MasseIVannbad { get; set; }  // Assuming Masse i vannbad is a decimal (double)
        public double Pw { get; set; }  // Assuming Pw is a decimal (double)
        public double V { get; set; }  // Assuming V is a decimal (double)
        public double Densitet { get; set; }  // Assuming Densitet is a decimal (double)

        public Guid DataFraOpdragsgiverId { get; set; }

        public DataFraOppdragsgiverPrøverModel DataFraOppdragsgiverPrøverModel { get; set; }

        public ConcreteDensityModel(int id, DateTime dato, double masseILuft, double masseIVannbad, double pw, double v, double densitet, Guid dataFraOpdragsgiverId)
        {
            Id = id;
            Dato = dato;
            MasseILuft = masseILuft;
            MasseIVannbad = masseIVannbad;
            Pw = pw;
            V = v;
            Densitet = densitet;
            DataFraOpdragsgiverId = dataFraOpdragsgiverId;
        }


    }
}

