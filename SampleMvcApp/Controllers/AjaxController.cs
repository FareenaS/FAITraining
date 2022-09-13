using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleMvcApp.Models;
namespace SampleMvcApp.Controllers
{
    public class AjaxController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public PartialViewResult EmpList()
        {
            var data = new RecapEntities().EmpTables.ToList();
            return PartialView(data);
        }

        public PartialViewResult AddNew()
        {
            var depts = new RecapEntities().DeptTables.ToList();
            var items = depts.Select((d) => new SelectListItem { Text = d.DeptName, Value = d.DeptId.ToString() }).ToList();
            ViewBag.Depts = items;
            return PartialView(new EmpTable());
        }
        [HttpPost]
        public RedirectToRouteResult AddNew(EmpTable rec)
        {
            var context = new RecapEntities();
            context.EmpTables.Add(rec);
            context.SaveChanges();
            return RedirectToAction("EmpList");
        }
    }
}