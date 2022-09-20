using DataComponentLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManager.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            if (Session["CurrentUser"] != null)
            {
                var cstDetails = Session["CurrentUser"] as CustomerTable;
                return "<h1>HOME PAGE</h1><hr/>Welcome to User: " + cstDetails.CustomerName;
            }
            else
                return "<h1>HOME PAGE</h1><hr/>Welcome to Anonymous User";
        }
    }
}