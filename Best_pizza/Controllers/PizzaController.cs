using MySql.Data.MySqlClient;
using Best_pizza.Models;
using Best_pizza.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Best_pizza.Controllers
{
    [Route("Pizza")]
    public class PizzaController : Controller
    {
        private PizzaService pizzaService = new PizzaService();

        [HttpGet]
        [Route("Pizza")]
        public ActionResult Index()
        {
            return RedirectToAction("ListPizza");
        }

        [HttpGet]
        [Route("ListPizza")]
        public ActionResult ListPizza()
        {
            return View(this.pizzaService.GetAllPizzasFromDb());
        }

        [HttpGet]
        [Route("CreatePizza")]
        public ActionResult CreatePizza()
        {
            ISet<Dough> doughs = this.pizzaService.GetAllDoughsFromDb();
            ISet<DoughType> doughTypes = this.pizzaService.GetAllDoughTypesFromDb();
            ISet<Topping> toppings = this.pizzaService.GetAllToppingsFromDb();

            Ingredients ingredients = new Ingredients();
            ingredients.Doughs = doughs;
            ingredients.DoughTypes = doughTypes;
            ingredients.Toppings = toppings;
            return View(ingredients);
        }

        [HttpPost]
        [Route("CreatePizza")]
        public ActionResult CreatePizzaConfirm()
        {
            var requestForm = Request.Form;
            Regex numberRegex = new Regex("^([a-zA-ZА-Яа-я]+)([0-9]+)$");
            Dough dough = new Dough();
            DoughType doughType = new DoughType();
            ISet<Topping> toppings = new HashSet<Topping>();
            foreach (var key in requestForm.Keys)
            {
                string keyStr = (string)key;
                if (requestForm[keyStr].Contains("true"))
                {
                    string type = numberRegex.Match(keyStr).Groups[1].ToString();
                    int ingredientId = int.Parse(numberRegex.Match(keyStr).Groups[2].ToString());
                    switch (type)
                    {
                        case "dough":
                            dough = this.pizzaService.GetDoughFromDb(ingredientId);
                            break;
                        case "doughType":
                            doughType = this.pizzaService.GetDoughTypeFromDb(ingredientId);
                            break;
                        case "topping":
                            Topping topping = this.pizzaService.GetToppingFromDb(ingredientId);
                            toppings.Add(topping);
                            break;
                    }
                }
            }
            Pizza pizza = new Pizza();
            pizza.Name = "CustomPizza";
            pizza.Dough = dough;
            pizza.DoughType = doughType;
            pizza.Toppings = toppings;
            this.pizzaService.SavePizzaToDb(pizza);
            return RedirectToAction("AddCreatedPizza", "Order", new { id = this.pizzaService.GetLastId() });
        }
    }
}