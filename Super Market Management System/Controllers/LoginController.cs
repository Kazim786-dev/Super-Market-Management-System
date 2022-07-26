using Microsoft.AspNetCore.Mvc;
using Super_Market_Management_System.Data;
using Super_Market_Management_System.Models;

namespace Super_Market_Management_System.Controllers
{
    public class LoginController : Controller
    {

        private ApplicationDbContext context;

        public LoginController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public static Login loginobj;

        //public LoginController( Login login)
        //{
        //    this.loginobj = login;
        //}


        
        public IActionResult Index()
        {
            return View("Login");
        }



        public IActionResult Verify()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            Login obj = new Login();
            return View("Login",obj);
        }

        [HttpPost]
        public IActionResult Login(Login log)
        {
            log.UName = HttpContext.Request.Form["username"].ToString();
            log.Password = HttpContext.Request.Form["email"].ToString();

            if (log.UName.Equals(log.AdminUName) && log.Password.Equals(log.AdminPassword))
            {
                loginobj = log;
                return RedirectToAction("Index","Home");
            }

            else
            {
                foreach ( var item in context.Cashier )
                {
                    if (item.username.Equals(log.UName) && item.email.Equals(log.Password) )
                    {
                        loginobj = log;
                        return RedirectToAction("cashierhome", "CashierSale");
                    }
                }

            }

            return View("Login");
        }


    }
}
