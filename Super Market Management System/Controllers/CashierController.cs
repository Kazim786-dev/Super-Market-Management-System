using Microsoft.AspNetCore.Mvc;
using Super_Market_Management_System.Data;
using Super_Market_Management_System.Models;

namespace Super_Market_Management_System.Controllers
{
    public class CashierController : Controller
    {
        private ApplicationDbContext context;

        public CashierController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult View()
        {
           // IEnumerable<Cashier> cashiers = context.Cashier;
           Cashier cashier = new Cashier();
           IEnumerable<Cashier> cashiers = cashier.ViewAll();

            return View("Index",cashiers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cashier cashier)
        {
            cashier.name = HttpContext.Request.Form["name"].ToString();
            cashier.email = HttpContext.Request.Form["email"].ToString();
            cashier.phone = HttpContext.Request.Form["phone"].ToString();
            cashier.username = HttpContext.Request.Form["username"].ToString();
            // DateTime dt1 = new DateTime();
            cashier.created_at = DateTime.Now;// dt1.ToString();

            //context.Cashier.Add(cashier);
            //context.SaveChanges();

            if (cashier.isExist(context, cashier.username,cashier.email))
            {
                TempData["ErrorMsg"] = "Username or Email Already exists";

                return RedirectToAction("View");
            }


            int result = cashier.SaveDetails();
            if (result == 1)
            {
                TempData["SuccessMsg"] = "Cashier successfully added";
            }
            else
            {
                TempData["ErrorMsg"] = cashier.name + " could not be added";
            }
            return RedirectToAction("View");
        }
        [HttpGet]
        [Route("Cashier/Edit/{Id:int}")]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var cashier = context.Cashier.Find(id);

            if (cashier == null)
            {
                return NotFound();
            }

            return View("Edit", cashier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cashier cashier)
        {
            cashier.cashierid = int.Parse(HttpContext.Request.Form["id"].ToString());

            cashier.name = HttpContext.Request.Form["name"].ToString();
            cashier.email = HttpContext.Request.Form["email"].ToString();
            cashier.phone = HttpContext.Request.Form["phone"].ToString();

            int result=cashier.Update();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Cashier info successfully updated";
            }
            else
            {
                TempData["ErrorMsg"] = cashier.name + " info could not be updated";
            }

            // context.Cashier.Update(cashier);
            //context.SaveChanges();
            return RedirectToAction("View");
        }

        

        [HttpGet]
        [Route("Cashier/Delete/{Id:int}")]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var cashier = context.Cashier.Find(id);
            if (cashier == null)
            {
                return NotFound();
            }
            ViewBag.Cashier = cashier;
            return View("Delete", cashier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult DeleteData(int? id)
        {
           // cashier.cashierid = int.Parse(HttpContext.Request.Form["id"].ToString());
             var cashier = context.Cashier.Find(id);
            if (cashier == null)
            {
                return NotFound();
            }

            int result=cashier.Delete();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Cashier info successfully deleted";
            }
            else
            {
                TempData["ErrorMsg"] = cashier.name + " info could not be deleted";
            }

            //context.Categories.Remove(category);
            //context.SaveChanges();

            return RedirectToAction("View");
        }


    }
}
