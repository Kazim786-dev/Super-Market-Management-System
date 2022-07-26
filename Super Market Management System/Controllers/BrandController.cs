using Microsoft.AspNetCore.Mvc;
using Super_Market_Management_System.Data;
using Super_Market_Management_System.Models;

namespace Super_Market_Management_System.Controllers
{
    public class BrandController : Controller
    {
        private ApplicationDbContext context;
        public BrandController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult View()
        {
            IEnumerable<Brand> Brands = context.brands; //IEnumerable doesn't return null but list does
            return View("ViewBrand", Brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateBrand");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult Create(Brand brand)
        {
            brand.BrandName = HttpContext.Request.Form["txtName"].ToString();

            if (brand.isExist(context, brand.BrandName))
            {
                TempData["ErrorMsg"]=brand.BrandName + " Already exists";

                return RedirectToAction("View");
            }


            brand.HeadOffice = HttpContext.Request.Form["txtOffice"].ToString();

            int result = brand.SaveDetails();

            TempData["SuccessMsg"] = "Brand successfully added";

            return RedirectToAction("View");
        }

        [HttpGet]
        [Route("Brand/Edit/{Id:int}")]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var brand = context.brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View("EditBrand", brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult Edit(Brand brand)
        {
            brand.BrandId = int.Parse(HttpContext.Request.Form["id"].ToString());
            brand.HeadOffice = HttpContext.Request.Form["txtOffice"].ToString();

            int result = brand.Update();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Brand successfully updated";
            }
            else
            {
                TempData["ErrorMsg"] = brand.BrandName + " could not be updated";
            }

            return RedirectToAction("View");
        }

        [HttpGet]
        [Route("Brand/Delete/{Id:int}")]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var brand = context.brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            ViewBag.brands = brand;
            return View("Delete", brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult DeleteData(int? id)
        {
            var brand = context.brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

           int result= brand.Delete();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Brand successfully deleted";
            }
            else
            {
                TempData["ErrorMsg"] = brand.BrandName + " could not be deleted";
            }

            return RedirectToAction("View");
        }
    }
}
