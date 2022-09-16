using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWebApi.ViewModels
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpAddress { get; set; }
        public decimal EmpSalary { get; set; }
        public int DeptId { get; set; }
        public string DateOfBirth { get; set; }
    }

    public class Department
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
    }
}