using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
namespace Super_Market_Management_System.Models

{
    public class Product
    {
       
        [Key]
        public int ProdId { get; set; }
        [Required]
        public string ProdName { get; set; } = string.Empty;

        [Required]
        public int ProdPrice { get; set; }
        [Required]
        [Range(1, 30, ErrorMessage = "The value must be between 1 and 30")]
        public int ProdQuantity { get; set; }

        [Required]
        [ForeignKey("categories")]
        public int Cat_Id { get; set; }

        public virtual Category categories { get; set; }

        [Required]
        [ForeignKey("brands")]
        public int BrandId { get; set; }
        public virtual Brand brands { get; set; }

        public Product(int prodId, string prodName, int prodPrice, int prodQuantity, int cat_Id, int brandId )
        {
            ProdId = prodId;
            ProdName = prodName;
            ProdPrice = prodPrice;
            ProdQuantity = prodQuantity;
            Cat_Id = cat_Id;
            BrandId = brandId;
        }

        public Product(string prodName,int prodQuantity)
        {
            ProdName = prodName;
            ProdQuantity = prodQuantity;
        }

        public Product()
        {
        }


        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            string query = "INSERT INTO Products(ProdName, ProdPrice, ProdQuantity, Cat_id, BrandId) values ('" + ProdName + "','" + ProdPrice + "', '"+ ProdQuantity+"', '" + Cat_Id + "','" + BrandId + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int Update()
        {
            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
            string query = "UPDATE Products SET ProdName= '" + ProdName + "',ProdPrice = '"+ProdPrice+ "' ,ProdQuantity = '" + ProdQuantity + "' WHERE ProdId= '" + ProdId + "'  ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int Delete()
        {

            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
            string query = "DELETE FROM Products WHERE ProdId= '" + ProdId + "'  ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }


        public List<Product> ViewAll()
        {

            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
            string query = "SELECT * FROM products";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            // IEnumerable<Product> list = new List<Product>();
            List<Product> products = new List<Product>();
            if (dr.HasRows)
            {
               // List<Product> products = new List<Product>();

                while (dr.Read())
                {
                    ProdId = int.Parse(dr["ProdId"].ToString());
                    ProdName = dr["ProdName"].ToString();
                    ProdPrice = Convert.ToInt32(dr["Prodprice"].ToString());
                    ProdQuantity = Convert.ToInt32(dr["ProdQuantity"].ToString());
                    BrandId = int.Parse(dr["BrandId"].ToString());
                    Cat_Id = int.Parse(dr["Cat_Id"].ToString());

                    products.Add(new Product(ProdId,ProdName,ProdPrice,ProdQuantity,Cat_Id,BrandId) );

                }
                //list = products;
            }


            con.Close();
            return products;
        }





    }
}
