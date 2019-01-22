using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace SearchAndDisplay.Models
{
    public class DBmodel
    {
        public SqlConnection DBConnection()
        { 
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Constring"].ConnectionString);
            return connection;
        }

    }
}