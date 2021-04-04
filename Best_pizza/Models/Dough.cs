using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Best_pizza.Models
{
    public class Dough
    {
        public Dough()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Calories { get; set; }

        public decimal Price { get; set; }

        public string Picture
        {
            get
            {
                return "dough" + this.Id + ".png";
            }
        }
    }
}