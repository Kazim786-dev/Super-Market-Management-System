using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Super_Market_Management_System.Data;

namespace Super_Market_Management_System.Models

{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        public string BrandName { get; set; }
        [Required]

        public string HeadOffice { get; set; }
        [Required]
        public virtual Product products { get; set; }

        public Brand(int brandId, string brandName, string headOffice)
        {
            BrandId = brandId;
            BrandName = brandName;
            HeadOffice = headOffice;
        }

        public Brand()
        {
        }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            string query = "INSERT INTO brands(BrandName,HeadOffice) values ('" + BrandName + "','" + HeadOffice + "')";
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
            string query = "UPDATE brands SET HeadOffice= '" + HeadOffice + "' WHERE BrandId= '" + BrandId + "'  ";
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
            string query = "DELETE FROM brands WHERE BrandId= '" + BrandId + "'  ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }


        public List<Brand> ViewAll()
        {

            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
            string query = "SELECT * FROM brands";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            // IEnumerable<Brand> list = new List<Brand>();
            List<Brand> brands = new List<Brand>();
            if (dr.HasRows)
            {
                // List<Brand> products = new List<Brand>();

                while (dr.Read())
                {
                    BrandId = int.Parse(dr["BrandId"].ToString());
                    BrandName = dr["BrandName"].ToString();
                    HeadOffice = dr["HeadOffice"].ToString();
                    
                    brands.Add(new Brand(BrandId, BrandName, HeadOffice));

                }
                //list = products;
            }


            con.Close();
            return brands;
        }


        public bool isExist(ApplicationDbContext context, string bn)
        {
            int ct = context.brands.Where( x=> x.BrandName.ToLower().Equals(bn.ToLower() ) ).Count();
            if (ct>0)
            {
                return true;
            }
            else 
                return false;

        }


    }
}
