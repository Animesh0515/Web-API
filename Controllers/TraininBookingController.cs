using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [AuthenticationFilter]
    public class TraininBookingController : ApiController
    {
        Utility utility = new Utility();

        [Route("api/TrainingBooking/ShowVenue")]
        [HttpGet]
        public List<string> ShowVenue()
        {
            List<string> venuelst = new List<string>();
            venuelst = utility.GetVenue();
            return venuelst;

        }


        [Route("api/TrainingBooking/ShowTime")]
        [HttpPost]
        public List<string> ShowTime([FromBody] string Venue)
        {
            List<string> timelst = new List<string>();
            timelst = utility.ShowTime(Venue);
            return timelst;
        }
    }
}