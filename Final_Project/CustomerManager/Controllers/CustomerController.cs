using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataComponentLib.Models;
using DataComponentLib;
using System.Web.Security;

namespace CustomerManager.Controllers
{
    [AllowAnonymous]
    public class CustomerController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var com = new UserComponent();
            var cst = com.ValidateUser(email, password);
            if(cst == null)
            {
                ModelState.AddModelError("LoginFailure", "Login failed for the user");
                return View();
            }
            Session["CurrentUser"] = cst;
            FormsAuthentication.SetAuthCookie(cst.EmailAddress, false);
            FormsAuthentication.RedirectFromLoginPage(cst.EmailAddress, false);
            return View();
        }
        public ActionResult RegisterNew()
        {
            CustomerTable model = new CustomerTable();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterNew(CustomerTable postedData)
        {
            IUserModule com = new UserComponent();
            try
            {
                com.RegisterUser(postedData);
                return RedirectToAction("Login");
            }
            catch(CustomerAlreadyExistsException ex)
            {
                ModelState.AddModelError("CustomerError", ex.Message);
                return View(postedData);
            }
        }
    }
}