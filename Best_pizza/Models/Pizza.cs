using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Best_pizza.Models
{
    public class Pizza
    {
        public Pizza()
        {
            this.Toppings = new HashSet<Topping>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Dough Dough { get; set; }

        public DoughType DoughType { get; set; }

        public ISet<Topping> Toppings { get; set; }

        public double Calories
        {
            get
            {
                return this.Dough.Calories + this.DoughType.Calories + this.Toppings.Select(t => t.Calories).Sum();
            }
        }

        public string ListOfToppings
        {
            get
            {
                return string.Join(", ", this.Toppings.Select(t => t.Name).ToArray());
            }
        }

        public string Picture
        {
            get
            {
                return "pizza" + this.Id + ".png";
            }
        }

        public decimal Price
        {
            get
            {
                return this.Dough.Price + this.DoughType.Price + this.Toppings.Select(t => t.Price).Sum();
            }
        }
    }
}