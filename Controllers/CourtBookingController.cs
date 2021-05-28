using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Windows.Forms;
using WebAPI.Models;
using static WebAPI.Models.ResponseModel;

namespace WebAPI.Controllers
{
    [AuthenticationFilter]
    public class CourtBookingController : ApiController
    {
        Utility utility = new Utility();
        

        [Route("api/CourtBooking/GetTimeList")]
        [HttpPost]
        public List<string>  GetTimeList([FromBody] BookingTimeRequestModel value)
        {
            List<string> timelst = new List<string>();
            if (value != null)
            {
                timelst = utility.GetTime(value);
                //MessageBox.Show(timelst[0].Time.ToString());
                return timelst;
            }
            else
            {
                timelst.Add("Error");
                return timelst;
            }

        }

        [Route("api/CourtBooking/BookCourt")]
        [HttpPost]
        public string BookCourt([FromBody] CourtBookingRequestModel response)
        {
            BookingResponse bookingResponse = new BookingResponse();
            bookingResponse.booked = utility.Booking(response);
            return JsonConvert.SerializeObject(bookingResponse);


        }
        [Route("api/CourtBooking/GetStatus")]
        [HttpGet]
        public string GetStatus()
        {
            CourtBookingStatusResponse statusResponse = new CourtBookingStatusResponse();
            statusResponse.Status  = utility.getStatus();
            return JsonConvert.SerializeObject(statusResponse);
        }

        [Route("api/CourtBooking/GetBookings")]
        [HttpGet]
        public string GetBookings()
        {
            List<MyBookingsModel> myBookingslst = new List<MyBookingsModel>();
            myBookingslst = utility.getBookings();
            return JsonConvert.SerializeObject(myBookingslst);
        }
    }
}