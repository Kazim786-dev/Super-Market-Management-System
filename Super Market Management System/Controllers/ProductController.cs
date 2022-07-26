using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Super_Market_Management_System.Data;
using Super_Market_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Super_Market_Management_System.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext context;
        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult View()
        {


            //IEnumerable<Product> products = context.products; //IEnumerable doesn't return null but list does
            //ViewBag.categories = context.Categories;
           // ViewBag.brands = context.brands;

            Product product = new Product();
            Category category = new Category();
            Brand brand = new Brand();
            //List<Product> tblproduct = context.products.ToList();
            //List<Category> tblcategory = context.Categories.ToList();
            //List<Brand> tblbrand = context.brands.ToList();

            List<Product> tblproduct = product.ViewAll();
            List<Category> tblcategory = category.ViewAll().ToList();
            List<Brand> tblbrand = brand.ViewAll().ToList();

            var result = (from p in tblproduct
                          join c in tblcategory on p.Cat_Id equals c.Id
                          join b in tblbrand on p.BrandId equals b.BrandId
                          select new CategoryProductViewModel
                          {
                              ProdId = p.ProdId,
                              ProdName = p.ProdName,
                              ProdPrice = p.ProdPrice,
                              ProdQuantity = p.ProdQuantity,
                              Name = c.Name,
                              BrandName = b.BrandName
                          }).ToList();


            return View("View",result);
        }

        [HttpGet]
        // [Route("Product/Add/{BrandId:int}")]
        public IActionResult Add()
        {
            // dynamic mymodel = new ExpandoObject();
            //mymodel.categories=context.Categories;
            //mymodel.Brands = context.brands;
            //   IEnumerable<Brand> brands = context.brands;
            //  return View("Create",brands);



            ViewBag.categories = context.Categories.ToList();
            ViewBag.brands = context.brands.ToList();
            var brands = context.brands.ToList().ToString();
            // ViewBag.brands = new List<String>() { "1","2", "3"};
            //ViewBag.brands = context.brands.ToList();

            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // value from form should not be null

        public async Task<IActionResult> Add(Product product)
        {
            var bid = HttpContext.Request.Form["brand"].ToString();
            var cid = HttpContext.Request.Form["cat"].ToString();
            var price = HttpContext.Request.Form["txtPrice"].ToString();
            var qua = HttpContext.Request.Form["txtquan"].ToString();
            product.BrandId = int.Parse(bid);
            product.Cat_Id = int.Parse(cid);
            product.ProdPrice = int.Parse(price);
            product.ProdName = HttpContext.Request.Form["txtName"].ToString();
            product.ProdQuantity = int.Parse(qua);

           int result= product.SaveDetails();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Product successfully added";
            }
            else
            {
                TempData["ErrorMsg"] = product.ProdName + " could not be added";
            }

            return RedirectToAction("View");
        }

        [HttpGet]
        [Route("Product/Edit/{Id:int}")]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var product = context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View("Edit",product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult Edit(Product product)
        {
            //if (product == null)
            //{
            //    return NotFound();
            //}
            product.ProdId = int.Parse(HttpContext.Request.Form["pid"].ToString());
            product.ProdName = HttpContext.Request.Form["txtname"].ToString();
            product.ProdPrice = Convert.ToInt32(HttpContext.Request.Form["txtprice"].ToString());

            int result = product.Update();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Product info successfully updated";
            }
            else
            {
                TempData["ErrorMsg"] = product.ProdName + " info could not be updated";
            }


            // context.Categories.Update(category);
            // context.SaveChanges();

            return RedirectToAction("View");
        }


        [HttpGet]
        [Route("Product/AddQuantity/{Id:int}")]
        public IActionResult AddQuantity(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var product = context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Product = product;
            return View("AddQuantity", product);
        }

        [HttpPost, ActionName("AddQuantity")]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult AddQuan(int? id)
        {
            //if (product == null)
            //{
            //    return NotFound();
            //}

            var product = context.products.Find(id);

            product.ProdQuantity = int.Parse(HttpContext.Request.Form["txtquan"].ToString());
            

            int result = product.Update();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Product Quantity successfully updated";
            }
            else
            {
                TempData["ErrorMsg"] = product.ProdName + " quantity could not be updated";
            }


            // context.Categories.Update(category);
            // context.SaveChanges();

            return RedirectToAction("View");
        }




        [HttpGet]
        [Route("Product/Delete/{Id:int}")]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }
            var product = context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Product = product;
            return View("Delete", product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult DeleteData(int? id)
        {
            var product = context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

           int result= product.Delete();

            if (result == 1)
            {
                TempData["SuccessMsg"] = "Product info successfully deleted";
            }
            else
            {
                TempData["ErrorMsg"] = product.ProdName + " info could not be deleted";
            }

            //context.Categories.Remove(category);
            //context.SaveChanges();

            return RedirectToAction("View");
        }






    }

}
