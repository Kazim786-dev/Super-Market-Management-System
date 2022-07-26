using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Super_Market_Management_System.Data;
using Super_Market_Management_System.Models;
using Rotativa.AspNetCore;
using System.Web.Mvc;
using Rotativa;
using SelectPdf;

namespace Super_Market_Management_System.Controllers
{
    public class OrderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private Order order;
        private Product product;
        private LoginController login;
        private ApplicationDbContext context;

        public static String Cname;
        public static String Cadd;
        public static String Cph;


        public OrderController(ApplicationDbContext context)
        {
            this.context = context;
     //       this.order = new Order();
        }
        
       


        [Microsoft.AspNetCore.Mvc.HttpGet]
//        [Route("Prod/Add/{BrandId:int}")]
        public IActionResult CreateOrder()
        {
            Order.products.Clear();
            IEnumerable<Order> orders = context.orders; //IEnumerable doesn't return null but list does
//            ViewBag.orderproduct = context.orderproduct;
            return View("CreateOrder");
        }




        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult CreateOrder(Order order)
        {
            this.order = order;
            Cname =HttpContext.Request.Form["customerName"].ToString();
            Cadd = HttpContext.Request.Form["customerAddress"].ToString();
            Cph = HttpContext.Request.Form["customerPhone"].ToString();
            this.order.order_generated_at = DateTime.Now;// dt1.ToString();

            Order.products.Clear();
       //     context.orders.Add(order);

    //        int result = order.SaveDetails();


            //context.SaveChanges();       

            return RedirectToAction("AddProduct",this.order);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        //        [Route("OrderProduct/AddProduct/{BrandId:int}")]
        public IActionResult AddProduct()
        {
            //     ViewBag.cashier = context.Cashier.ToList();
            //     ViewBag.customer = context.Customer.ToList();
            //            ViewBag.bill = context.bills.ToList();
        //    ViewBag.categories = context.Categories.ToList();
        //    ViewBag.product = context.products.ToList();
        //    ViewBag.order = context.orders.ToList();

            return View("AddProduct");
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]  // value from form should not be null
        public IActionResult AddProduct(Order order)
        {
            this.product = new Product();

            var pName = HttpContext.Request.Form["productName"].ToString();
            //var price = HttpContext.Request.Form["txtPrice"].ToString();
            var qua = Convert.ToInt32(HttpContext.Request.Form["totalQuantity"].ToString());
            //this.product.ProdName = pName;  
            //this.product.ProdQuantity = int.Parse(qua);

            //    this.order.products.Add(1, pName, 1000, qua, 1, 1) ;
            //        this.order.products.Add(qua);
            //            orderproduct.SaveDetails();
            var id = 0;
            foreach(var p in context.products)
            {
                if (p.ProdName.Equals(pName))
                {
                    id = p.ProdId;
                }
            }

            SqlConnection con = new SqlConnection("Data Source=DELLCOREI7\\SQLEXPRESS;Initial Catalog=SSMS_DB;Integrated Security=True");
            string query = "SELECT * FROM Products WHERE ProdId='"+id+"'";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                // List<Product> products = new List<Product>();

                while (dr.Read())
                {
                    this.product.ProdId = int.Parse(dr["ProdId"].ToString());
                    this.product.ProdName = dr["ProdName"].ToString();
                    this.product.ProdPrice = Convert.ToInt32(dr["Prodprice"].ToString());
                    this.product.ProdQuantity = Convert.ToInt32(dr["ProdQuantity"].ToString());
                    this.product.BrandId = int.Parse(dr["BrandId"].ToString());
                    this.product.Cat_Id = int.Parse(dr["Cat_Id"].ToString());

                    if (Convert.ToInt32(dr["ProdQuantity"].ToString())<0)
                    {
                        TempData["ErrorMsg"] = this.product.ProdName + " Temporarily not available";
                        return View(order);
                    }

                    if ( this.product.ProdQuantity < qua  )
                    {
                        TempData["ErrorMsg"] = " Not enough quantity. Available is : "+ this.product.ProdQuantity;

                        return RedirectToAction("AddProduct");
                    }

                    if ( qua<1 )
                    {

                        return RedirectToAction("AddProduct");
                    }


                }
                this.product.ProdQuantity = qua;
                Order.products.Add(this.product);
                TempData["SuccessMsg"] = pName + " added to cart";

                return RedirectToAction("AddProduct");
            }
            else
            {
                TempData["ErrorMsg"] = pName + " Not exist";

                return RedirectToAction("AddProduct");

            }
            
            con.Close();

            //Order order1 = new Order();
            return View(order);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult displayCart()
        {

            if (Order.products.Count==0)
            {
                return RedirectToAction("AddProduct");
            }



            var x = 0;

            foreach(var p in Order.products)
            {
                x += p.ProdPrice * (p.ProdQuantity) ;
            }
            ViewBag.Total=x;
            return View("Cart", Order.products);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]  // value from form should not be null
        public Microsoft.AspNetCore.Mvc.ActionResult payment(Order order)

        {
            //  this.order = order;
            //   var pName = HttpContext.Request.Form["productName"].ToString();
            //var price = HttpContext.Request.Form["txtPrice"].ToString();
            //   var qua = HttpContext.Request.Form["totalQuantity"].ToString();
            //  this.order.products.Add(1,pName,1000,qua,2,3);
            //    this.order.products.Add(1, pName, 1000, qua, 1, 1) ;
            //        this.order.products.Add(qua);
            //            orderproduct.SaveDetails();
            var x = 0;
            Order o = new Order();
            foreach(var p in Order.products)
            {
                x += p.ProdPrice * (p.ProdQuantity);
            }
            o.totalPrice = x;
            foreach(var c in context.Cashier)
            {
                if (c.email.Equals(LoginController.loginobj.Password) )
                {
                    o.cashierid = c.cashierid;
                }
            }
            
            //o.customerName = Order.customerName;
            //o.customerPhone = this.order.customerPhone;
            //o.customerAddress = this.order.customerAddress;
            o.order_generated_at = DateTime.Now;

            Order or = new Order(Cname,Cadd,Cph,x,o.cashierid);

            context.orders.Add(or);
            context.SaveChanges();

            foreach(var p in Order.products)
            {
                OrderProduct op = new OrderProduct();

                op.orderid = or.orderid;
                op.ProdId = p.ProdId;
                op.quantity = p.ProdQuantity;
                context.orderproduct.Add(op);
                context.SaveChanges();

                Product pro = context.products.Find(op.ProdId);
                pro.ProdQuantity -= p.ProdQuantity;
                context.products.Update(pro);
                context.SaveChanges();
            }

            

            TempData["SuccessMsg"] =" Successfully placed ";
            ViewBag.custname = Cname;
            ViewBag.custphone = Cph;
            ViewBag.cashname = context.Cashier.Find(o.cashierid).name.ToString();
            ViewBag.cashid = o.cashierid;
            ViewBag.Total = o.totalPrice;

            return View("Bill", Order.products);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("Order/delete/{Id:int}")]
        public IActionResult delete(int? id)
        {
            if (id == 0 || id.Equals(null))
            {
                return NotFound();
            }

            foreach(var item in Order.products)
            {
                if (item.ProdId == id)
                {
                    Order.products.Remove(item);
                    break;
                }
            }


            return RedirectToAction("displayCart");

        }
        public System.Web.Mvc.ActionResult printreceipt()
        {
            var printpdf = new Rotativa.ViewAsPdf("Bill");

            return printpdf;

        }


        public System.Web.Mvc.ActionResult Print(/*String html*/)
        {
            //html = html.Replace("StrTag", "<").Replace("EndTag", ">");
            //HtmlToPdf oHtmlToPdf = new HtmlToPdf();
            //PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html);
            //byte[] pdf = oPdfDocument.Save();
            //oPdfDocument.Close();
            //return File(
            //    pdf,
            //    "bill/pdf",
            //    "Receipt.pdf"
            //    );
            
            return new Rotativa.ViewAsPdf("Bill",Order.products);
        }


    }
}
