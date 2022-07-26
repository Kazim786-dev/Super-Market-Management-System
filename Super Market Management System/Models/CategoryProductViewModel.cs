namespace Super_Market_Management_System.Models
{
    public class CategoryProductViewModel
    {
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public int ProdPrice { get; set; }
        public int ProdQuantity { get; set; }
        public int Cat_Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }


        public List<Product> product { get; set; }
        public List<Category> categories { get; set; }
        public List<Brand> brands { get; set; }


    }
}
