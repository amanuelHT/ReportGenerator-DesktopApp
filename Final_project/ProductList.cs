using System.Collections;

namespace Final_project
{
    public class ProductList
    {
        public Guid Id { get; set; }
        public string Tittle { get; set; }
        public bool Status { get; set; }
        public string Kunde { get; set; }

        // Other properties (if needed) can go here

        public static IList GetData()
        {
            List<ProductList> datas = new List<ProductList>();
            ProductList data;

            // Sample data - modify according to your actual needs
            data = new ProductList()
            {

                Tittle = "reportOne",
                Status = true,
                Kunde = "Teklit"
            };
            datas.Add(data);

            data = new ProductList()
            {

                Tittle = "reportOnesd",
                Status = false,
                Kunde = "amanuel"
            };
            datas.Add(data);



            //data = new ProductList()
            //{
            //    Id = Guid.NewGuid(),
            //    Tittle = "ReportTwo",
            //    Status = false,
            //    Kunde = "Amanuel"
            //};
            //datas.Add(data);

            // Add more items as needed...

            return datas;
        }
    }
}
