using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Super_Market_Management_System.Data;

namespace Super_Market_Management_System.Models
{
    public class Cashier
    {
        [Required]
        [Key]
        public int cashierid { get; set; }
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

        public Cashier(int cashierid, string name, string username, string email, string phone, DateTime created_at)
        {
            this.cashierid = cashierid;
            this.name = name;
            this.username = username;
            this.email = email;
            this.phone = phone;
            this.created_at = created_at;
        }

        public Cashier()
        {
        }


        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            string query = "INSERT INTO Cashier(name,username,email,phone,created_at) values ('" + name + "','" + username + "' ,'" + email + "' ,'" + phone + "' ,'" + created_at + "'  )";
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
            string query = "UPDATE Cashier SET name= '" + name + "',email = '" + email + "', phone = '" + phone + "' WHERE cashierid= '" + cashierid + "'  ";
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
            string query = "DELETE FROM Cashier WHERE cashierid= '" + cashierid + "'  ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }


        public IEnumerable<Cashier> ViewAll()
        {

            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
            string query = "SELECT * FROM Cashier";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            IEnumerable<Cashier> list = new List<Cashier>();

            if (dr.HasRows)
            {
                List<Cashier> cashiers = new List<Cashier>();

                while (dr.Read())
                {
                    cashierid = Convert.ToInt32(dr["cashierid"].ToString());
                    name = dr["name"].ToString();
                   email = dr["email"].ToString();
                   created_at = Convert.ToDateTime(dr["created_at"]);
                   username = dr["username"].ToString();
                   phone = dr["phone"].ToString();

                    cashiers.Add(new Cashier(cashierid, name, username, email, phone,created_at));

                }
                list = cashiers;
            }


            con.Close();
            return list;
        }


        public bool isExist(ApplicationDbContext context, string un, string mail )
        {
            int ct = context.Cashier.Where(x => x.name.ToLower().Equals(un.ToLower())).Count();
            ct = context.Cashier.Where(x => x.email.ToLower().Equals(mail.ToLower())).Count();
            if (ct > 0)
            {
                return true;
            }
            else
                return false;

        }


    }
}
