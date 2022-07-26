using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Super_Market_Management_System.Models
{
    public class Supplier
    {
        [Required]
        [Key]
        public int supplierid { get; set; }
        [Required]
        public String name { get; set; } = string.Empty;
        [Required]
        public String username { get; set; } = string.Empty;
        [Required]
        public String email { get; set; } = string.Empty;

        [Required]
        public String phone { get; set; } = string.Empty;
        [Required]
        public DateTime created_at { get; set; }


        public int Update()
        {
            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
            string query = "UPDATE Supplier SET name= '" + name + "',email = '" + email + "', phone = '" + phone + "' WHERE supplierid= '" + supplierid + "'  ";
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
            string query = "DELETE FROM Supplier WHERE supplierid= '" + supplierid + "'  ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}
