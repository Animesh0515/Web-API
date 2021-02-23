﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
    }

    public class ValidatedLogin
    {
        public bool validate { get; set; }
    }
}