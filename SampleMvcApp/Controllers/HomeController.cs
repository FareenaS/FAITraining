using SampleMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public string HelloWorld()
        {
            return "Hello world from the sample App!!!";
        }

        public Employee HaiEmployee()
        {
            return new Employee { EmpId = 123, EmpName = "Phaniraj" };
        }

        public ViewResult DisplayEmployee()
        {
            //create a model object
            var emp = new Employee { EmpId = 123, EmpName = "Phaniraj" };
            return View(emp);//inject the model to the view.
        }
    }
}