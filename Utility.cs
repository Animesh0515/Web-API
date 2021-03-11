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
    public class Utility
    {
        string connectionstring = DatabaseConnectionModel.SqlConnection();

        public List<LoginRequestModel> login()
        {
            DataTable dt = new DataTable();
            List<LoginRequestModel> loginRequestlst = new List<LoginRequestModel>();
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
                    LoginRequestModel loginRequestModel = new LoginRequestModel();
                    loginRequestModel.User_ID = int.Parse(fetch_query["User_Id"].ToString());
                    loginRequestModel.Username = fetch_query["Username"].ToString();
                    loginRequestModel.Password = fetch_query["Password"].ToString();
                    loginRequestlst.Add(loginRequestModel);
                }
                conn.Close();
                return loginRequestlst;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return null;
            }


        }

        public int Signup (UserModel users)
        {
            //string dateString = String.Format("{0:dd/MM/yyyy}", users.DateOfBirth);
            int Status;
            MySqlConnection conn = new MySqlConnection(connectionstring);
            string validatequery = "select COUNT(*) from users where username='" + users.Username + "'";
            MySqlCommand cmd = new MySqlCommand(validatequery, conn);
            try
            {
                conn.Open();
                var output = cmd.ExecuteScalar();
                int count = int.Parse(output.ToString());
                if (count == 0)
                {
                    string userdetailquery = "Insert into users (`Role`, `First_Name`, `Last_Name`, `Email`, `Phone_Number`, `DateOfBirth`, `Password`, `Gender`, `Age`, `JoinedDate`, `Username`)  values('User','" + users.First_Name + "','" + users.Last_Name + "','" + users.Email + "','" + users.Phone_Number + "','" + String.Format("{0:yyyy/MM/dd}", users.DateOfBirth) + "','" + users.Password + "','" + users.Gender + "','" + users.Age + "','" + String.Format("{0:yyyy/MM/dd}", users.JoinedDate) + "','" + users.Username + "');";
                    MySqlCommand Insertcmd = new MySqlCommand(userdetailquery, conn);
                    Insertcmd.ExecuteNonQuery();
                    Status = 1;
                    conn.Close();
                     return Status;

                }
                else
                {
                    Status = 0;
                    conn.Close();
                    return Status;
                }
                

            }
            catch (Exception e)
            {
                Status = 2;
                return Status;

            }
        }

        public UserResponseModel GetUserDetails(int Id)
        {
            UserResponseModel userResponse = new UserResponseModel();
            string User_Query = "Select * from users where User_Id='" + Id + "';";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = new MySqlCommand(User_Query, conn);
            //MySqlCommand User_Query = conn.CreateCommand();
            //User_Query.CommandText = "Select * from users where User_Id='" + Id + "';";
            conn.Open();
            MySqlDataReader QueryResult =cmd.ExecuteReader();
            while(QueryResult.Read())
            {
                userResponse.First_Name = QueryResult["First_Name"].ToString();
                userResponse.Last_Name = QueryResult["Last_Name"].ToString();
                userResponse.Email = QueryResult["Email"].ToString();
                userResponse.Phone_Number = Convert.ToInt32(QueryResult["Phone_Number"]);
                userResponse.DateOfBirth = DateTime.Parse(QueryResult["DateOfBirth"].ToString());
                userResponse.Gender = QueryResult["Gender"].ToString();
                userResponse.Age = Convert.ToInt32(QueryResult["Age"]);
                userResponse.JoinedDate = DateTime.Parse(QueryResult["JoinedDate"].ToString());
                return userResponse;

            }
            return null;

        }

    }
}
