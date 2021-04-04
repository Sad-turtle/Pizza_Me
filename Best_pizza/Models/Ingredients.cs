using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Best_pizza.Models
{
    public class Ingredients
    {
        public Ingredients()
        {
            this.Doughs = new SortedSet<Dough>();
            this.DoughTypes = new SortedSet<DoughType>();
            this.Toppings = new SortedSet<Topping>();
        }

        public ISet<Dough> Doughs { get; set; }

        public ISet<DoughType> DoughTypes { get; set; }

        public ISet<Topping> Toppings { get; set; }

        public string PizzaDough { get; set; }

        public string PizzaDoughType { get; set; }


    }
}