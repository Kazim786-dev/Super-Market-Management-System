using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace Super_Market_Management_System.Models
{
    public class Order
    {
        [Key]
        public int orderid { get; set; }

        [Required]
        public DateTime order_generated_at { get; set; }

        [Required]
        public string customerName { get; set; } = string.Empty;

        [Required]
        public string customerAddress  { get; set; } = string.Empty;

        [Required]
        public string customerPhone { get; set; } = string.Empty;
       
        public int totalPrice { get; set; }

        public static List<Product> products = new List<Product>();


        [Required]
        [ForeignKey("Cashier")]
        public int cashierid { get; set; }
        public virtual Cashier cashier { get; set; }


        public Order(string name,string add,string phone, int t, int id)
        {
            customerAddress = add;
            customerName = name;
            customerPhone = phone;
            totalPrice = t;
            order_generated_at = DateTime.Now;
            this.cashierid = id;
        }

        public Order()
        {

        }




       // public int SaveDetails()
       // {
       //     SqlConnection con = new SqlConnection("Data Source=DESKTOP-2AV6505\\SQLEXPRESS;Initial Catalog=Super_Market_Management_System_DB;Integrated Security=True");
       //     string query = "INSERT INTO orders(order_generated_at, customerName, customerAddress, customerPhone) values ('" + order_generated_at + "', '" + customerName + "', '" + customerAddress + "','" + customerPhone + "')";
       //     SqlCommand cmd = new SqlCommand(query, con);
       //     con.Open();
       //     int i = cmd.ExecuteNonQuery();
       //     con.Close();
       //     return i;
       // }

       // public int Update()
       // {
       //     SqlConnection con = new SqlConnection("Data Source=DESKTOP-2AV6505\\SQLEXPRESS;Initial Catalog=Super_Market_Management_System_DB;Integrated Security=True");
       //     // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
       //  //   string query = "UPDATE Products SET ProdName= '" + ProdName + "',ProdPrice = '" + ProdPrice + "' ,ProdQuantity = '" + ProdQuantity + "' WHERE ProdId= '" + ProdId + "'  ";
       //  //   SqlCommand cmd = new SqlCommand(query, con);
       //     con.Open();
       // //    int i = cmd.ExecuteNonQuery();
       //     con.Close();
       //     //   return i;
       //     return 0;
       // }

       // public int Delete()
       // {

       //     SqlConnection con = new SqlConnection("Data Source=DESKTOP-2AV6505\\SQLEXPRESS;Initial Catalog=Super_Market_Management_System_DB;Integrated Security=True");
       //     // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
       //  //   string query = "DELETE FROM Products WHERE ProdId= '" + ProdId + "'  ";
       // //    SqlCommand cmd = new SqlCommand(query, con);
       //     con.Open();
       ////     int i = cmd.ExecuteNonQuery();
       //     con.Close();
       //     //  return i;
       //     return 0;
       // }

    }
}
