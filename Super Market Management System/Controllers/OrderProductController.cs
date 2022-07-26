using Microsoft.AspNetCore.Mvc;
using Super_Market_Management_System.Data;
using Super_Market_Management_System.Models;

namespace Super_Market_Management_System.Controllers
{
    public class OrderProductController : Controller
    {
        private ApplicationDbContext context;
        public OrderProductController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //public IActionResult CreateOrder()
        //{
        //    IEnumerable<BillProduct> billproduct = context.billproduct; //IEnumerable doesn't return null but list does
        //    ViewBag.cashier = context.Cashier;
        //    ViewBag.customer = context.Customer;
        //    ViewBag.bill = context.bills;
        //    ViewBag.product = context.products;

        //    //var pro = @item;  // need this! really
        //    //var product = @ViewBag.categories.Where(cat => cat.Id.ToString().Contains(@item.Cat_Id.ToString()));

        //    List<Bill> tblbill = context.bills.ToList();
        //    //    List<Cashier> tblcashier = context.Cashier.ToList();
        //    //    List<Customer> tblcustomer = context.Customer.ToList();
        //    List<BillProduct> tblbillproduct = context.billproduct.ToList();
        //    List<Product> tblproduct = context.products.ToList();

        //    var result = (from bp in tblbillproduct
        //                  join b in tblbill on bp.invoiceid equals b.invoiceid
        //                  join p in tblproduct on bp.ProdId equals p.ProdId
        //                  select new BillProductViewModel
        //                  {
        //                      cashierid = b.cashier.cashierid,
        //                      invoiceid = b.invoiceid,
        //                      invoice_generated_at = b.invoice_generated_at,
        //                      name = b.customer.name,
        //                      address = b.customer.address,
        //                      phone = b.customer.phone,
        //                      ProdName = p.ProdName,
        //                      ProdPrice = p.ProdPrice,
        //                      ProdQuantity = p.ProdQuantity,
        //                      totalPrice = b.totalPrice,
        //                      grandTotal = b.grandTotal,

        //                  }).ToList();


        //    return View(result);
        //}

    }
}
//foreach (var item in getWPDetails)
//{
//    Viewmodel = new ProductDetailModel();
//    Viewmodel.Productid = item.Productid;
//    Viewmodel.ProductName = item.ProductName;
//    Viewmodel.CategoryID = item.CategoryID;
//    Viewmodel.ProductRate = item.ProductRate;
//    Viewmodel.DiscountRate = item.DiscountRate;
//    Viewmodel.imageurl1 = item.imageurl1;
//    WPDetails.Add(Viewmodel);
//}