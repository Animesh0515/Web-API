using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ResponseModel
    {
        public class ValidatedLogin
        {
            public bool validate { get; set; }
        }

        public class ValidateSignup
        {
            public int Status { get; set; }
        }

        public class BookingResponse
        {
            public bool booked { get; set; }
        }

        public class ProfileUpdateResponse
        {
            public bool updated { get; set; }
        }

        public class CourtBookingStatusResponse
        {
            public string Status { get; set; }
        }
    }
}