using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Best_pizza.Models
{
    public class Registration
    {
        public Registration()
        {

        }

        public int id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
    }
}