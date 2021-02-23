using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class DatabaseConnection
    {
        public static string Server { get; set; }
        public static string Database { get; set; }
        public static string  Username { get; set; }
        public static string  Password { get; set; }

        public static string SqlConnection()
        {
            return "Server="+Server+";Port=3306;Database=" + Database + ";Uid=" + Username + ";Pwd=" + Password + ";";
        }
    }
}