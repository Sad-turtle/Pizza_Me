using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Best_pizza.Models
{
    public class Order
    {
        public Order()
        {
            this.Pizzas = new List<Pizza>();
        }

        public int Id { get; set; }

        public IList<Pizza> Pizzas { get; set; }

        public decimal Price { get; set; }
    }
}