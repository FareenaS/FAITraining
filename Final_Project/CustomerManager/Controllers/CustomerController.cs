using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataComponentLib.Models;
using DataComponentLib;

namespace CustomerManager.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
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
                return RedirectToAction("RegisterNew");
            }
            catch(CustomerAlreadyExistsException ex)
            {
                ModelState.AddModelError("CustomerError", ex.Message);
                return View(postedData);
            }
        }
    }
}