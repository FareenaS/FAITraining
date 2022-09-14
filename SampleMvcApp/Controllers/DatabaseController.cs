using SampleMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMvcApp.Controllers
{
    [Authorize]
    public class DatabaseController : Controller
    {
        public ViewResult AllEmployees()
        {
            var context = new RecapEntities();
            var model = context.EmpTables.ToList();
            return View(model);
        }

        //Get the matching Employee based on the Id selected.
        public ViewResult OnEdit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("ID is not set");
            }
            var empId = int.Parse(id);
            var context = new RecapEntities();
            //var model = (from emp in context.EmpTables
            //             where emp.EmpId == empId``   
            //             select emp).FirstOrDefault();
            var model = context.EmpTables.FirstOrDefault((e) => e.EmpId == empId);
            if (model == null)
                throw new Exception("No Employee found with matching ID");
            return View(model);
        }

        [HttpPost]
        public RedirectToRouteResult OnEdit(EmpTable updatedRec)
        {
            //connect to the database
            var context = new RecapEntities();
            //get the record matching the Id
            var rec = context.EmpTables.FirstOrDefault((e) => e.EmpId == updatedRec.EmpId);
            if(rec == null)
            {
                throw new Exception("Record is not found");
            }
            rec.EmpName = updatedRec.EmpName;
            rec.EmpAddress = updatedRec.EmpAddress;
            rec.EmpSalary = updatedRec.EmpSalary;
            rec.DeptId = updatedRec.DeptId;
            rec.DateOfBirth = updatedRec.DateOfBirth;
            context.SaveChanges();//Updates the data to the database. 
            return RedirectToAction("AllEmployees");
        }

        public RedirectToRouteResult OnDelete(string id)
        {
            //convert the string to no
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("ID of the Employee is not set to delete");
            }
            var empId = int.Parse(id);
            //create the context object
            var context = new RecapEntities();
            //find the matching Employee from the database
            var emp = context.EmpTables.Find(empId);
            if (emp == null) throw new Exception("Employee not found with the matching ID");
            context.EmpTables.Remove(emp);
            context.SaveChanges();//Commits the transaction to the database
            return RedirectToAction("AllEmployees");
        }
        //Every action can have only one Model. To pass additional data to the View, U should either use ViewBag, ViewData or TempData. Both ViewBag and ViewData refer to the same object called Dictionary
        public ActionResult AddNewEmployee()
        {
            var newRec = new EmpTable();
            ViewBag.Depts = GetDeptItems();//Set the List<SelectListItem> to the ViewBag.             
            return View(newRec);//Generate the View....
        }

        /// <summary>
        /// Helper Function to convert Dept Data into List<SelectListItem>
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        private List<SelectListItem> GetDeptItems()
        {
            var context = new RecapEntities();
            var depts = context.DeptTables.ToList();//Get All Depts from the database.
            var pairs = new Dictionary<string, int>();//To resolve the Duplicate DeptNames....
            foreach (var dept in depts)
                pairs[dept.DeptName] = dept.DeptId;
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var pair in pairs)
                items.Add(new SelectListItem { Text = pair.Key, Value = pair.Value.ToString() });
            return items;
        }

        [HttpPost]
        public ActionResult AddNewEmployee(EmpTable inputData)
        {
            var context = new RecapEntities();
            context.EmpTables.Add(inputData);
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }

        public ActionResult Search()
        {
            return View();//No model for this View..
        }

        public ActionResult SearchByName(string search)
        {
            var context = new RecapEntities();
            var records = context.EmpTables.Where((e) => e.EmpName.Contains(search)).ToList();
            ViewData["SearchedEmpList"] = records;
            return View("Search");//
        }
    }
}
