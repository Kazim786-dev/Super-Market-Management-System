using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Super_Market_Management_System.Data;

namespace Super_Market_Management_System.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Product> products { get; set; }

        public Category(int Id, string name)
        {
            this.Id = Id;
            this.Name = name;
        }

        public Category()
        {

        }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            string query = "INSERT INTO Categories(Name) values ('" + Name + "')";
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
            string query = "UPDATE Categories SET Name= '"+ Name+ "' WHERE Id= '" +Id+ "'  ";
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
            string query = "DELETE FROM Categories WHERE Id= '" + Id + "'  ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }


        public IEnumerable<Category> ViewAll()
        {

            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            // sql = "Update demotb set TutorialName='"VB.Net Complete"+"' where TutorialID=3";
            string query = "SELECT * FROM Categories";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
           SqlDataReader dr = cmd.ExecuteReader();

            IEnumerable<Category> list = new List<Category>();

            if (dr.HasRows)
            {
                List<Category> categories = new List<Category>();

                while (dr.Read())
                {
                    Id = int.Parse(dr["Id"].ToString());
                    Name = dr["Name"].ToString();

                    categories.Add(new Category(Id, Name));
                    
                }
                list = categories;
            }


            con.Close();
            return list;
        }


        public bool isExist(ApplicationDbContext context, string cat)
        {
            int ct = context.Categories.Where(x => x.Name.ToLower().Equals(cat.ToLower())).Count();
            if (ct > 0)
            {
                return true;
            }
            else
                return false;

        }



    }
}
