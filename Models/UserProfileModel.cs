using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class UserData{
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string ImageUrl { get; set; }

    }
    public class UserProfileModel
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public Int32 Phone_Number { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Int32 Age { get; set; } 
        public string ImageUrl { get; set; }
        
    }
    
}