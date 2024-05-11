using Domain.Models;

namespace Report_Generator_Domain.Models
{
    public class ConcreteDensityModel
    {
        public Guid Id { get; set; }
        public int Prøvenr { get; set; }

        public DateTime Dato { get; set; }
        public double MasseILuft { get; set; }
        public double MasseIVannbad { get; set; }
        public double Pw { get; set; }
        public double V { get; set; }
        public double Densitet { get; set; }

        public Guid ReportModelId { get; set; }
        public ReportModel ReportModel { get; set; }

        public ConcreteDensityModel(Guid id, int prøvenr, DateTime dato, double masseILuft, double masseIVannbad, double pw, double v, double densitet, Guid reportModelId)
        {
            Id = id;
            Prøvenr = prøvenr;
            Dato = dato;
            MasseILuft = masseILuft;
            MasseIVannbad = masseIVannbad;
            Pw = pw;
            V = v;
            Densitet = densitet;
            ReportModelId = reportModelId;
        }


    }
}
