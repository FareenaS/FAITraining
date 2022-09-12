using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleWebFormApp
{
    public partial class CachingDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblTime.Text = DateTime.Now.ToLongTimeString();
            string city = Request["City"];
            switch (city)
            {
                case "London": lblTime.Text = DateTime.Now.AddHours(-5.5).ToLongTimeString();break;
                case "Sydney": lblTime.Text = DateTime.Now.AddHours(4.5).ToLongTimeString(); break;
                case "New Delhi": lblTime.Text = DateTime.Now.ToLongTimeString(); break;
                case "New York": lblTime.Text = DateTime.Now.AddHours(-11.5).ToLongTimeString(); break;
                case "San Francisco": lblTime.Text = DateTime.Now.AddHours(-12.5).ToLongTimeString(); break;
                default:
                    lblTime.Text = DateTime.Now.ToLongTimeString();
                    break;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var url = "CachingDemo.aspx?City=" + cmbCity.Text;
            Response.Redirect(url);
        }
    }
}