using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class LoginRequestModel
    {
        public int User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}