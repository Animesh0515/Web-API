using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    

    public class UserModel
    {

        //public int UserId { get; set; }
        //public string Role { get; set; } = "user";
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public Int32 Phone_Number { get; set; }

        //[DefaultValueAttribute(typeof(DateTime), "0")]
        public DateTime? DateOfBirth { get; set; } = null;
        public string Password { get; set; }
        public string Gender { get; set; }
        public Int32 Age { get; set; } 
        public DateTime JoinedDate { get; set; } = DateTime.Today;
        public string Username { get; set; }
        
        
    }

   
}