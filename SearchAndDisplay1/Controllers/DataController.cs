using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SearchAndDisplay.Models;
using System.Configuration;

namespace SearchAndDisplay.Controllers
{
    public class DataController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        //displaying database
        [HttpGet]
        public JsonResult Display()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\727761\source\repos\SearchAndDisplay\SearchAndDisplay\App_Data\EmpInfo.mdf;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from Employee", con);
            cmd.ExecuteNonQuery();
            List<Model1> DisplayList = new List<Model1>();
            Model1 m;
            using (SqlDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    m = new Model1();
                    m.EmpId = int.Parse(read["EmpId"].ToString());
                    m.Name = read["Name"].ToString();
                    m.Age = int.Parse(read["Age"].ToString());
                    m.Designation = read["Designation"].ToString();
                    m.ExpYears = int.Parse(read["ExpYears"].ToString());
                    DisplayList.Add(m);

                }
            }

            return Json(DisplayList,JsonRequestBehavior.AllowGet);

        }

        //Searching the element
        [HttpGet]
        public JsonResult Search(int EmpId)

        {
            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\727761\source\repos\SearchAndDisplay\SearchAndDisplay\App_Data\EmpInfo.mdf;Integrated Security=True");
            con1.Open();
            string q = "SELECT * from Employee WHERE EmpId='" + EmpId + "'";
            SqlCommand cmd1 = new SqlCommand(q, con1);
            cmd1.ExecuteNonQuery();
            List<Model1> SearchItem = new List<Model1>();
            Model1 n = new Model1();
            using (SqlDataReader read = cmd1.ExecuteReader())
            {
                while (read.Read())
                {

                    n.EmpId = int.Parse(read["EmpId"].ToString());
                    n.Name = read["Name"].ToString();
                    n.Age = int.Parse(read["Age"].ToString());
                    n.Designation = read["Designation"].ToString();
                    n.ExpYears = int.Parse(read["ExpYears"].ToString());
                    SearchItem.Add(n);

                }

            }
            con1.Close();
            return Json(SearchItem, JsonRequestBehavior.AllowGet);
        }

        //for dropdown
        [HttpGet]
        public JsonResult Dropdown(string Designation)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\727761\source\repos\SearchAndDisplay\SearchAndDisplay\App_Data\EmpInfo.mdf;Integrated Security=True");
            conn.Open();
            string q = "SELECT * from Employee WHERE Designation='" + Designation + "'";
            SqlCommand cmd1 = new SqlCommand(q, conn);
            cmd1.ExecuteNonQuery();
            List<Model1> DropList = new List<Model1>();
            Model1 n = new Model1();
            SqlDataReader dr;
            dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                DropList.Add(new Model1()
                {
                    EmpId = dr.GetInt32(dr.GetOrdinal("EmpId")),
                    Name = dr.GetString(dr.GetOrdinal("Name")),
                    Age = dr.GetInt32(dr.GetOrdinal("Age")),
                    Designation = dr.GetString(dr.GetOrdinal("Designation")),
                    ExpYears = dr.GetInt32(dr.GetOrdinal("ExpYears"))
                });

            }
            dr.Close();
            conn.Close();
            return Json(DropList, JsonRequestBehavior.AllowGet);
        }


    }
}