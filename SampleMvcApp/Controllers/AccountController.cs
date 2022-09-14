using SampleMvcApp.Models;
using SampleMvcApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SampleMvcApp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult RegisterNewUser()
        {
            UserDetails model = new UserDetails();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterNewUser(UserDetails user)
        {
            //Check for the validation. If Valid =>
            if (ModelState.IsValid && IsValidUser(user.EmailAddress))
            {
                //Connect to the database
                var context = new RecapEntities();
                UserTable rec = new UserTable
                { EmailAddress = user.EmailAddress, FirstName = user.FirstName, LastName = user.LastName, Password = user.Password };//Convert the ViewModel object to Model object. 
                context.UserTables.Add(rec); //Insert the new record
                context.SaveChanges(); 
                ViewBag.Message = "User Successfully registered with Us!!!";
                //Redirect to the Home Page
                return RedirectToAction("Index");
            }else//If the validation fails. 
            {
                ModelState.AddModelError("Failure", "User already Exists");
                return View();
            }           
        } 

        private bool IsValidUser(string emailAddress)
        {
            //Connect to the Database
            var context = new RecapEntities();
            //Check for the record matching the email Address
            var rec = context.UserTables.FirstOrDefault((user) => user.EmailAddress == emailAddress);
            //if Exists, return false else return true. 
            if (rec == null) return true;
            return false;
        }

        public ActionResult Login()
        {
            var model = new LoginView();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginView loginDetails)
        {
            if (ModelState.IsValid)
            {
                var context = new RecapEntities();
                var user = context.UserTables.SingleOrDefault((u) => u.EmailAddress == loginDetails.EmailAddress && u.Password == loginDetails.Password);
                if (user == null)
                {
                    ModelState.AddModelError("LoginFailure", "Login Failed for the User");
                    return View(loginDetails);
                }
                    FormsAuthentication.SetAuthCookie(loginDetails.EmailAddress, false);
                    FormsAuthentication.RedirectFromLoginPage(loginDetails.EmailAddress, false);
                    return View(loginDetails);
            }
            else
                return View(loginDetails);
        }
    }
}