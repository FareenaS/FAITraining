using DataComponentLib;
using DataComponentLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManager.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var allProducts = new List<ProductTable>();
            
            return View(allProducts);
        }

        public ActionResult OnAddToCart(ProductTable item)
        {
            var cart = new List<ProductTable>();//create a new cart...
            if(Session["cart"] != null)//Check if the cart already exists
            {
                cart = Session["cart"] as List<ProductTable>;//Get the items of the cart
            }
            cart.Add(item);
            Session["cart"] = cart;//BOX it 
            return RedirectToAction("Index");
        }

        public ActionResult GoToBill()
        {
            var currentUser = Session["currentUser"] as CustomerTable;
            var com = new CartComponent();
            com.Customer = currentUser;
            com.GenerateBill();
            return RedirectToAction("Index");
        }
    }
}