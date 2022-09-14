using SampleMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMvcApp.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Home()
        {
            return View();
        }

        public PartialViewResult GetAllEmployees()
        {
            var data = new RecapEntities().EmpTables.ToList();
            return PartialView(data);
        }

        #region Manipulations
        public PartialViewResult AddNewEmployee()
        {
            var depts = new RecapEntities().DeptTables.ToList();//Get the Depts Lisgt
            var selectListItems = depts.Select((d) => new SelectListItem { Text = d.DeptName, Value = d.DeptId.ToString() }).ToList();//Convert every Dept to SelectListItem
            ViewBag.AllItems = selectListItems;//Set the List<SelectListItem> to the ViewBag by name AllItems
            return PartialView(new EmpTable());//Return the Partial View....
        }

        [HttpPost]
        public ActionResult AddNewEmployee(EmpTable rec)
        {
            var context = new RecapEntities();
            context.EmpTables.Add(rec);//Add to the collection data
            context.SaveChanges();//Commit to the database.
            return RedirectToAction("GetAllEmployees");
        }

        public ActionResult Edit(string id)
        {
            var empId = Convert.ToInt32(id);
            var context = new RecapEntities();
            var selected = context.EmpTables.FirstOrDefault((e) => e.EmpId == empId);
            if (selected == null) throw new Exception("Failed to get the record");
            return PartialView(selected);
        }
        [HttpPost]
        public ActionResult Edit(EmpTable updatedRec)
        {
            var context = new RecapEntities();
            var selected = context.EmpTables.FirstOrDefault((e) => e.EmpId == updatedRec.EmpId);
            if (selected == null) throw new Exception("Failed to get the record to Update");
            selected.DeptId = updatedRec.DeptId;
            selected.DateOfBirth = updatedRec.DateOfBirth;
            selected.EmpAddress = updatedRec.EmpAddress;
            selected.EmpSalary = updatedRec.EmpSalary;
            selected.EmpName = updatedRec.EmpName;
            context.SaveChanges();
            return RedirectToAction("GetAllEmployees");
        }

        #endregion
    }
}