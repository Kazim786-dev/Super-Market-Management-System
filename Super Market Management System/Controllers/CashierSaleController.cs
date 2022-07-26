using Microsoft.AspNetCore.Mvc;

namespace Super_Market_Management_System.Controllers
{
    public class CashierSaleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cashierhome()
        {
            return View("CashierHome");
        }

    }
}
