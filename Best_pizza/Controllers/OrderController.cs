using Best_pizza.Models;
using Best_pizza.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Best_pizza.Controllers
{
    public class OrderController : Controller
    {
        private static IList<Pizza> pizzas = new List<Pizza>();

        private PizzaService pizzaService = new PizzaService();
        private OrderService orderService = new OrderService();

        [Route("Order")]
        public ActionResult Index()
        {
            return RedirectToAction("ListOrder");
        }

        [Route("ListOrder")]
        public ActionResult ListOrder()
        {
            return View(pizzas);
        }

        [HttpGet]
        [Route("AddPizza/{id}")]
        public ActionResult AddPizza(int id)
        {
            Pizza pizza = this.pizzaService.GetPizzaFromDbById(id);
            return View(pizza);
        }

        [HttpPost]
        [Route("AddPizza/{id}")]
        public ActionResult AddPizzaConfirmed(int id)
        {
            Pizza pizza = this.pizzaService.GetPizzaFromDbById(id);
            pizzas.Add(pizza);
            return RedirectToAction("ListPizza", "Pizza");
        }

        [HttpGet]
        [Route("AddCreatedPizza/{id}")]
        public ActionResult AddCreatedPizza(int id)
        {
            Pizza pizza = this.pizzaService.GetCreatedPizzaFromDbById(id);
            return View(pizza);
        }

        [HttpPost]
        [Route("AddCreatedPizza/{id}")]
        public ActionResult AddCreatedPizzaConfirmed(int id)
        {
            Pizza pizza = this.pizzaService.GetCreatedPizzaFromDbById(id);
            pizzas.Add(pizza);
            return RedirectToAction("ListPizza", "Pizza");
        }
        [HttpPost]
        [Route("ListOrder")]
        public ActionResult CompleteOrder()
        {
            Order order = new Order();
            order.Pizzas = pizzas;
            order.Price = pizzas.Select(p => p.Price).Sum();
            this.orderService.AddOrderToDb(order);
            pizzas.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}