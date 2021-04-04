using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Best_pizza.Models;
using Best_pizza.Services;
using MySql.Data.MySqlClient;

namespace Best_pizza.Controllers
{
    public class RegisterController : Controller
    {
        private RegisterService registerService = new RegisterService();
        

        [HttpGet]
        [Route("AddOrEdit")]
        public ActionResult AddOrEdit(int id = 0)
        {
            Register newUser = new Register();
            return View(newUser);
        }

        [HttpPost]
        [Route("AddOrEdit")]
        public ActionResult AddOrEdit(Register newUser)
        {

            this.registerService.RegisterUser(newUser);
            
            ModelState.Clear();
            
            return RedirectToAction("Login");

        }

        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            Register newUser = new Register();
            return View(newUser);

        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(Register newUser)
        {


            if (this.registerService.GetPassword(newUser.Email) == newUser.Password)
            {
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                
                return RedirectToAction("AddOrEdit", "Register");
            }
        }



    }
}