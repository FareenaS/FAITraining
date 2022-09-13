using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleMvcApp.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }

        public override string ToString()
        {
            return string.Format("<h1>The Name {0} with ID {1}", EmpName, EmpId);
        }
    }
}