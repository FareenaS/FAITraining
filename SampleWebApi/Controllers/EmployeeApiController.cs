using SampleWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleWebApi.Models;

namespace SampleWebApi.Controllers
{
    public class EmployeeApiController : ApiController
    {
        public List<Employee> GetAllEmployees()
        {
            var context = new FaiTrainingEntities();
            var records = context.EmpTables.ToList();//Get all records
            List<Employee> employees = new List<Employee>();//Create a blank List<Employee>
            foreach(var e in records)//Iterate thru the records
            {
                var emp = new Employee//convert each rec to Employee object
                {
                    DateOfBirth = e.DateOfBirth.HasValue ? e.DateOfBirth.Value.ToLongDateString() : "Not Available",
                    DeptId = e.DeptId.HasValue ? e.DeptId.Value : 0,
                    EmpAddress = e.EmpAddress,
                    EmpName = e.EmpName,
                    EmpSalary = e.EmpSalary,
                    EmpID = e.EmpId
                };
                employees.Add(emp);//Add the converted Employee object to List
            }
            return employees;//return the list
        }

        public Employee GetEmployee(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            var empId = Convert.ToInt32(id);
            var context = new FaiTrainingEntities();
            var selected = context.EmpTables.Where((e) => e.EmpId == empId).FirstOrDefault();
            if (selected == null) return null;
            var emp = new Employee
            {
                DateOfBirth = selected.DateOfBirth.HasValue ? selected.DateOfBirth.Value.ToLongDateString() : "Not Available", 
                DeptId = selected.DeptId.HasValue ? selected.DeptId.Value : 0,
                EmpAddress = selected.EmpAddress, EmpID = selected.EmpId, EmpName = selected.EmpName, EmpSalary = selected.EmpSalary
            };
            return emp;
        }

        public string AddEmployee(Employee emp)
        {
            //convert the emp object(ViewModel) to EmpTable object(Model)
            var model = new EmpTable
            {
                DateOfBirth = Convert.ToDateTime(emp.DateOfBirth),
                DeptId = emp.DeptId,
                EmpAddress = emp.EmpAddress,
                EmpName = emp.EmpName,
                EmpSalary = emp.EmpSalary
                //EmpID is auto generated, so dont assign it....
            };
            var context = new FaiTrainingEntities();//create the context object
            context.EmpTables.Add(model);//Add the model object into the context
            context.SaveChanges();//Save changes
            return "Employee added to the database";
        }

        [HttpPut]
        public string UpdateEmployee(Employee emp)
        {
            var context = new FaiTrainingEntities();
            var selected = context.EmpTables.FirstOrDefault((e) => e.EmpId == emp.EmpID);
            if (selected == null) return "Employee not found!!!!";
            selected.DateOfBirth = Convert.ToDateTime(emp.DateOfBirth);
            selected.DeptId = emp.DeptId;
            selected.EmpAddress = emp.EmpAddress;
            selected.EmpName = emp.EmpName;
            selected.EmpSalary = emp.EmpSalary;
            context.SaveChanges();
            return "Employee updated!!!!";
        }

        [HttpDelete]
        public string DeleteEmployee(string id)
        {
            var empId = Convert.ToInt32(id);
            var context = new FaiTrainingEntities();
            var rec = context.EmpTables.FirstOrDefault((e) => e.EmpId == empId);
            context.EmpTables.Remove(rec);
            context.SaveChanges();
            return "Employee removed from the database!!!!";
        }
    }
}
