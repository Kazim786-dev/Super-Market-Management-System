using Microsoft.AspNetCore.Mvc;
using Super_Market_Management_System.Data;
using Super_Market_Management_System.Models;

namespace Super_Market_Management_System.Controllers
{
    public class CategoryController : Controller
    {

        private ApplicationDbContext context;

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
           return View();
        }

        public IActionResult View()
        {
            //IEnumerable<Category> categories = context.Categories; //IEnumerable doesn't return null but list does
            Category category = new Category();
            IEnumerable<Category> categories = category.ViewAll();
            return View("ViewCategory", categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult Create(Category category)
        {
            category.Name = HttpContext.Request.Form["txtName"].ToString();


            if (category.isExist(context, category.Name))
            {
                TempData["ErrorMsg"] = category.Name + " Already exists";

                return RedirectToAction("View");
            }


            int result = category.SaveDetails();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Category successfully added";
            }
            else
            {
                TempData["ErrorMsg"] = category.Name + " could not be added";
            }


            //context.Categories.Add(category);
            //context.SaveChanges();

            return RedirectToAction("View");
        }

        [HttpGet]
        [Route("Category/Edit/{Id:int}")]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id.Equals(null) )
            {
                return NotFound(); 
            }
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View("EditCategory",category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult Edit(Category category)
        {
            category.Name = HttpContext.Request.Form["txtName"].ToString();

            int result=category.Update();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Category successfully updated";
            }
            else
            {
                TempData["ErrorMsg"] = category.Name + " could not be updated";
            }


            // context.Categories.Update(category);
            // context.SaveChanges();

            return RedirectToAction("View");
        }

        [HttpGet]
        [Route("Category/Delete/{Id:int}")]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.Category = category;
            return View("Delete", category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult DeleteData(int? id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            int result=category.Delete();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Category successfully deleted";
            }
            else
            {
                TempData["ErrorMsg"] = category.Name + " could not be deleted";
            }



            //context.Categories.Remove(category);
            //context.SaveChanges();

            return RedirectToAction("View");
        }







    }
}
