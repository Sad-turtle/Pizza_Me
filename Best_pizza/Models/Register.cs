﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Best_pizza.Models
{
    public class Register
    {
        public Register()
        {
            
        }
        public int id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address  { get; set; }

        public string Phone  { get; set; }

    }
}