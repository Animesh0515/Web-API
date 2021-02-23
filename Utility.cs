using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI
{
    public  class Utility
    {
        string connectionstring = DatabaseConnection.SqlConnection();

        public  List<Account> login()
        {
            DataTable dt = new DataTable();
            List<Account> lstaccount = new List<Account>();
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand query = conn.CreateCommand();
            query.CommandText = "Select * from users;";
            //string query = "Select * from users;";
            //MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();
                //var da = new MySqlDataAdapter(cmd);
                //da.Fill(dt);
                MySqlDataReader fetch_query = query.ExecuteReader();
                while (fetch_query.Read())
                {
                    Account account = new Account();
                    account.Username = fetch_query["Username"].ToString();
                    account.Password = fetch_query["Password"].ToString();
                    lstaccount.Add(account);
                }
                return lstaccount;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return null;
            }


        }
        
    }
}