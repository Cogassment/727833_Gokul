﻿using System;
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
            DBmodel Dataconnection = new DBmodel();
            SqlConnection connection = Dataconnection.DBConnection();
            connection.Open();
            SqlCommand comd = new SqlCommand("SELECT * from Employee", connection);
            List<Employeemodel> DisplayList = new List<Employeemodel>();
            Employeemodel DBlist;
            using (SqlDataReader read = comd.ExecuteReader())
            {
                while (read.Read())
                {
                    DBlist = new Employeemodel();
                    DBlist.EmpId = int.Parse(read["EmpId"].ToString());
                    DBlist.Name = read["Name"].ToString();
                    DBlist.Age = int.Parse(read["Age"].ToString());
                    DBlist.Designation = read["Designation"].ToString();
                    DBlist.ExpYears = int.Parse(read["ExpYears"].ToString());
                    DisplayList.Add(DBlist);

                }
            }

            return Json(DisplayList,JsonRequestBehavior.AllowGet);

        }

        //Searching the element
        [HttpGet]
        public JsonResult Search(int EmpId)

        {
            DBmodel Dataconnection = new DBmodel();
            SqlConnection connection = Dataconnection.DBConnection();
            connection.Open();
            string select = "SELECT * from Employee WHERE EmpId='" + EmpId + "'";
            SqlCommand comd = new SqlCommand(select, connection);
            List<Employeemodel> SearchItem = new List<Employeemodel>();
            Employeemodel Searchresult = new Employeemodel();
            using (SqlDataReader read = comd.ExecuteReader())
            {
                while (read.Read())
                {

                    Searchresult.EmpId = int.Parse(read["EmpId"].ToString());
                    Searchresult.Name = read["Name"].ToString();
                    Searchresult.Age = int.Parse(read["Age"].ToString());
                    Searchresult.Designation = read["Designation"].ToString();
                    Searchresult.ExpYears = int.Parse(read["ExpYears"].ToString());
                    SearchItem.Add(Searchresult);

                }

            }
            connection.Close();
            return Json(SearchItem, JsonRequestBehavior.AllowGet);
        }

        //for dropdown
        [HttpGet]
        public JsonResult Dropdown(string Designation)
        {
            DBmodel Dataconnection = new DBmodel();
            SqlConnection connection = Dataconnection.DBConnection();
            connection.Open();
            string select = "SELECT * from Employee WHERE Designation='" + Designation + "'";
            SqlCommand comd = new SqlCommand(select, connection);
            List<Employeemodel> DropList = new List<Employeemodel>();
            SqlDataReader datareader;
            datareader = comd.ExecuteReader();
            while (datareader.Read())
            {
                DropList.Add(new Employeemodel()
                {
                    EmpId = datareader.GetInt32(datareader.GetOrdinal("EmpId")),
                    Name = datareader.GetString(datareader.GetOrdinal("Name")),
                    Age = datareader.GetInt32(datareader.GetOrdinal("Age")),
                    Designation = datareader.GetString(datareader.GetOrdinal("Designation")),
                    ExpYears = datareader.GetInt32(datareader.GetOrdinal("ExpYears"))
                });

            }
            datareader.Close();
            connection.Close();
            return Json(DropList, JsonRequestBehavior.AllowGet);
        }


    }
}
