using DataComponentLib.DataLayer;
using DataComponentLib.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;
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

            if (!IsPostBack)
            {
                if (Cache["MyData"] == null)
                {
                    string strCon = ConfigurationManager.ConnectionStrings["myCon"].ConnectionString;
                    SqlCacheDependencyAdmin.EnableNotifications(strCon);
                    SqlCacheDependencyAdmin.EnableTableForNotifications(strCon, "EmpTable");
                    SqlCacheDependency dep = new SqlCacheDependency("CACHE-DEMO", "EmpTable");
                    Response.Write("New data is retrived");
                    var data = DataFactory.GetEmployeeManager().GetAllEmployees();
                    Cache.Add("MyData",//Key to the cache, should be unique 
                        data, //data to cache, obtained from the db in this example
                        //new System.Web.Caching.CacheDependency(Server.MapPath("MyTextFile.txt")),//Cache is removed if this file is modified.
                        dep,
                        DateTime.Now.AddMinutes(5),//Time interval to cache.
                        System.Web.Caching.Cache.NoSlidingExpiration,//No Extension of the time. 
                        System.Web.Caching.CacheItemPriority.Default, //Default priority
                        null);//No call back function that should be called after caching. 
                }
                else
                    Response.Write("Cached Data is retrived");
                lstNames.DataSource = DataFactory.GetEmployeeManager().GetAllEmployees();
                //lstNames.DataSource = Cache["MyData"] as List<Employee>;
                lstNames.DataTextField = "EmpName";
                lstNames.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var url = "CachingDemo.aspx?City=" + cmbCity.Text;
            Response.Redirect(url);
        }
    }
}