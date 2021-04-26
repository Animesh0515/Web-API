using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebAPI.Models;

namespace WebAPI
{
    public class Utility
    {
        string connectionstring = DatabaseConnectionModel.SqlConnection();
        static DateTime dateTime = DateTime.Now;
        string bookedDate = dateTime.ToString("yyyy-MM-dd");

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

        public int Signup(UserModel users)
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
                    string userdetailquery = "Insert into users (`Role`, `First_Name`, `Last_Name`, `Email`, `Phone_Number`, `DateOfBirth`, `Password`, `Gender`, `Age`, `JoinedDate`, `Username`)  values('User','" + users.First_Name + "','" + users.Last_Name + "','" + users.Email + "','" + users.Phone_Number + "','" + String.Format("{0:yyyy-MM-dd}", users.DateOfBirth) + "','" + users.Password + "','" + users.Gender + "','" + users.Age + "','" + String.Format("{0:yyyy/MM/dd}", users.JoinedDate) + "','" + users.Username + "');";
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

        public UserProfileModel GetUserDetails(int Id)
        {
            UserProfileModel userResponse = new UserProfileModel();
            string User_Query = "Select * from users where User_Id='" + Id + "';";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = new MySqlCommand(User_Query, conn);
            //MySqlCommand User_Query = conn.CreateCommand();
            //User_Query.CommandText = "Select * from users where User_Id='" + Id + "';";
            try
            {
                conn.Open();
                MySqlDataReader QueryResult = cmd.ExecuteReader();
                while (QueryResult.Read())
                {
                    userResponse.First_Name = QueryResult["First_Name"].ToString();
                    userResponse.Last_Name = QueryResult["Last_Name"].ToString();
                    userResponse.Email = QueryResult["Email"].ToString();
                    userResponse.Phone_Number = Convert.ToInt32(QueryResult["Phone_Number"]);
                    userResponse.Address = QueryResult["Address"].ToString();
                    userResponse.DateOfBirth = DateTime.Parse(QueryResult["DateOfBirth"].ToString());
                    userResponse.Gender = QueryResult["Gender"].ToString();
                    userResponse.Age = Convert.ToInt32(QueryResult["Age"]);
                    userResponse.ImageUrl = QueryResult["ImageUrl"].ToString();

                }
                conn.Close();
                return userResponse;


            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
            return null;
        }

        public List<string> GetTime(BookingTimeRequestModel value)
        {
            List<string> timelst = new List<string>();
            string User_Query = "Select * from timetable;";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = new MySqlCommand(User_Query, conn);
            try
            {
                conn.Open();
                MySqlDataReader QueryResult = cmd.ExecuteReader();
                while (QueryResult.Read())
                {

                    timelst.Add(QueryResult["Time"].ToString());

                }
                timelst = checkTime(timelst, value.Date);
                conn.Close();
                return timelst;

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }

        }

        public List<string> checkTime(List<string> lst, string date)
        {
            List<string> checklst = new List<string>();
            List<string> finallst = new List<string>();

            string User_Query = "Select Time from courtbooking where Booked_for='" + date + "'";
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = new MySqlCommand(User_Query, conn);
            try
            {
                conn.Open();
                MySqlDataReader QueryResult = cmd.ExecuteReader();
                while (QueryResult.Read())
                {

                    checklst.Add(QueryResult["Time"].ToString());

                }
                if (checklst.Count != 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        finallst.Add(lst[i]);
                        for (int j = 0; j < checklst.Count; j++)
                        {



                            if (lst[i] == checklst[j])
                            {
                                //string time = lst[i];
                                finallst.Remove(lst[i]);
                                // break;
                            }
                        }
                    }
                    conn.Close();
                    return finallst;
                }
                else
                {
                    return lst;
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public bool Booking(CourtBookingRequestModel response)
        {

            bool booked;

            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into courtbooking(User_Id,Booked_date,Booked_for,Time) values (" + WebApiApplication.User_Id + ",'" + bookedDate + "','" + response.date + "','" + response.time + "')";
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                booked = true;
                conn.Close();
                return booked;
            }
            catch (Exception e)
            {
                Console.Write(e);
                booked = false;
                return booked;
            }

        }

        public List<string> GetVenue()
        {
            List<string> venuelst = new List<string>();
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select DISTINCT(Venue) from trainingdetails";
            try
            {
                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    venuelst.Add(dr["Venue"].ToString());
                }
                conn.Close();
                return venuelst;


            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }

        }

        public List<string> ShowTime(string venue)
        {
            List<string> timelst = new List<string>();
            MySqlConnection conn = new MySqlConnection(connectionstring);
            string query = "Select Distinct(Time) from trainingdetails where Venue='" + venue + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    timelst.Add(reader["Time"].ToString());

                }
                conn.Close();
                return timelst;

            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }

        }

        public bool BookCourt(TrainingBookingRequestModel value)
        {
            MySqlConnection conn = new MySqlConnection(connectionstring);
            string query = "Inser into trainingbooking (Booked_Date,Booked_For,Time) values('" + bookedDate + "','" + value.JoiningDate + "','" + value.Time + "') ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception e)
            {
                Console.Write(e);
                return false;
            }
        }
        public List<CalendarDataResponseModel> GetData(int weekday)
        {
            List<CalendarDataResponseModel> calendarDatalst = new List<CalendarDataResponseModel>();
            string day = "";
            switch (weekday)
            {
                case 1:
                    day = "Monday";
                    break;
                case 2:
                    day = "Tuesday";
                    break;
                case 3:
                    day = "Wednesday";
                    break;
                case 4:
                    day = "Thursday";
                    break;
                case 5:
                    day = "Friday";
                    break;
                case 6:
                    day = "Saturday";
                    break;
                case 7:
                    day = "Sunday";
                    break;
            }
            MySqlConnection conn = new MySqlConnection(connectionstring);
            string query = "Select Time,Venue from trainingdetails where day='" + day + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CalendarDataResponseModel calendarData = new CalendarDataResponseModel();
                    calendarData.time = reader["Time"].ToString();
                    calendarData.Venue = reader["Venue"].ToString();
                    calendarDatalst.Add(calendarData);
                }
                return calendarDatalst;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<string> GetPhotos()
        {
            List<string> photos = new List<string>();
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select Media_Url from gallery where Media_Type='Image'";
            try
            {
                conn.Open();
                MySqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    photos.Add(data["Media_Url"].ToString());
                }
                return photos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool UpdateImage(string ImageUrl)
        {
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE  users set ImageUrl='" + ImageUrl + "' WHERE User_Id=" + WebApiApplication.User_Id + "";
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool UpdateUserDetails(UserProfileModel userdetails)
        {
            MySqlConnection conn = new MySqlConnection(connectionstring);
            string query = "UPDATE users SET First_Name='" + userdetails.First_Name + "', Last_Name='" + userdetails.Last_Name + "', Email='" + userdetails.Email + "',Phone_Number='" + userdetails.Phone_Number + "',DateOfBirth='" + String.Format("{0:yyyy-MM-dd}", userdetails.DateOfBirth) + "',Gender='" + userdetails.Gender + "',Age='" + userdetails.Age + "',Address='" + userdetails.Address + "' where User_Id=" + WebApiApplication.User_Id + "; ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public string getStatus()
        {

            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from mobilebookingfeature";
            try
            {
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    return rdr["Status"].ToString();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return null;
        }

        public List<MyBookingsModel> getBookings()
        {
            List<MyBookingsModel> bookingslst = new List<MyBookingsModel>();
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from courtbooking where User_Id=" + WebApiApplication.User_Id + " order by Booked_Date desc";
            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())

                {
                    MyBookingsModel myBookings = new MyBookingsModel();
                    myBookings.BookingType = "Court";
                    myBookings.BookedFor = reader["Booked_for"].ToString();
                    myBookings.BookedDate = reader["Booked_Date"].ToString();
                    myBookings.Time = reader["Time"].ToString();
                    bookingslst.Add(myBookings);


                }
                conn.Close();
                string query = "SELECT * from trainingbooking tb LEFT join trainingbookingvenue tbv on tb.Training_Booking_Id=tbv.training_booking_id where tbv.User_Id=" + WebApiApplication.User_Id + ";";
                conn.Open();

                MySqlCommand selectcmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = selectcmd.ExecuteReader();

                while (rdr.Read())
                {
                    MyBookingsModel myBookings = new MyBookingsModel();
                    myBookings.BookingType = "Training";
                    myBookings.BookedFor = String.Format("{0:yyyy-MM-dd}", rdr["Booked_for"]);
                    myBookings.BookedDate = String.Format("{0:yyyy-MM-dd}", rdr["Booked_Date"]);
                    myBookings.Time = rdr["Time"].ToString();
                    myBookings.Venue = rdr["Venue"].ToString();
                    bookingslst.Add(myBookings);

                }
                conn.Close();
                var ascendingOrder = bookingslst.OrderBy(i => i.BookedDate);
                return ascendingOrder.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public List<NotificationModel> getNotification()
        {

            List<NotificationModel> notificationslst = new List<NotificationModel>();
            MySqlConnection conn = new MySqlConnection(connectionstring);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from usernotification";
            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())

                {
                    NotificationModel notification = new NotificationModel();
                    notification.Notification = reader["notification"].ToString();
                    notificationslst.Add(notification);
                }
                return notificationslst;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

