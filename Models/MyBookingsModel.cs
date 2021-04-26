using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class MyBookingsModel
    {
        public string BookingType { get; set; }
        public string BookedFor { get; set; }
        public string BookedDate { get; set; }
        public string Time { get; set; }
        public string  Venue { get; set; }
    }
}