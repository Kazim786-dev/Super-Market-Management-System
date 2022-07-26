namespace Super_Market_Management_System.Models
{
    public class Login
    {
        
        public String AdminUName { get; set; } = "admin";
        public String AdminPassword { get; set; } = "admin@gmail.com";

        public String UName { get; set; }

        public String Password { get; set; }


        public Login()
        {

        }

        public Login(string un, string pass)
        {
            this.UName = un;
            this.Password = pass;

        }


    }
}
