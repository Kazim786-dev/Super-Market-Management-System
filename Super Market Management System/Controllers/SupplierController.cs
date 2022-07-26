using Microsoft.AspNetCore.Mvc;
using Super_Market_Management_System.Data;
using Super_Market_Management_System.Models;

namespace Super_Market_Management_System.Controllers
{
    public class SupplierController : Controller
    {
        private ApplicationDbContext context;
        public SupplierController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult View()
        {
            IEnumerable<Supplier> suppliers= context.Supplier; //IEnumerable doesn't return null but list does
            return View("Index", suppliers);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier)
        {
            supplier.name = HttpContext.Request.Form["name"].ToString();
            supplier.email = HttpContext.Request.Form["email"].ToString();
            supplier.phone = HttpContext.Request.Form["phone"].ToString();
            supplier.username = HttpContext.Request.Form["username"].ToString();
            // DateTime dt1 = new DateTime();
            supplier.created_at = DateTime.Now;// dt1.ToString();

            context.Supplier.Add(supplier);
            context.SaveChanges();
            // int result = cashier.SaveDetails();

            return RedirectToAction("View");
        }
        [HttpGet]
        [Route("Supplier/Edit/{Id:int}")]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var supplier = context.Supplier.Find(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View("Edit", supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Supplier supplier)
        {
            supplier.supplierid = int.Parse(HttpContext.Request.Form["id"].ToString());

            supplier.name = HttpContext.Request.Form["name"].ToString();
            supplier.email = HttpContext.Request.Form["email"].ToString();
            supplier.phone = HttpContext.Request.Form["phone"].ToString();

            supplier.Update();

            // context.Cashier.Update(cashier);
            //context.SaveChanges();
            return RedirectToAction("View");
        }



        [HttpGet]
        [Route("Supplier/Delete/{Id:int}")]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var supplier = context.Supplier.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            ViewBag.Supplier = supplier;
            return View("Delete", supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult DeleteData(int? id)
        {
            // cashier.cashierid = int.Parse(HttpContext.Request.Form["id"].ToString());
            var supplier = context.Supplier.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            supplier.Delete();
            //context.Categories.Remove(category);
            //context.SaveChanges();

            return RedirectToAction("View");
        }


    }
}

    
