using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicWorld.Data;
using MusicWorld.Services.Contracts;
using MusicWorld.View.Models;

namespace MusicWorld.Controllers
{
    public class UsersController : Controller
    {
        private IUserServices services;
        private readonly MusicWorldContext context;
        public UsersController(IUserServices services, MusicWorldContext context)
        {
            this.services = services;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string username, string password, string firstName, string lastName)
        {
            this.services.CreateAccount(username, password, firstName, lastName);

            return RedirectToAction("Home", "Index");
        }


        public IActionResult Logout()
        {
            services.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var result = services.Login(username, password);
                 
                if (result.Contains("not found"))
                {
                    return RedirectToAction("Create", "Users");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                Response.StatusCode = 404;
                return View("UserNotFound",username);
            }
        }

        [HttpGet]
        public IActionResult All(string sortOrder)
        {
            var usr= context.Users.AsQueryable();
            ViewData["NameOrder"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch (sortOrder)
            {
                case "name_desc":
                    usr = usr.OrderByDescending(x => x.Username);
                    break;
                default:
                    usr = usr.OrderBy(x => x.Username);
                    break;
            }//context.Users.ToList()

            return View(usr.ToList());
        }

        [HttpGet(Name = "Delete")]
        public IActionResult Delete(string id)
        {
            var isTrue = services.Delete(id);

            if (isTrue == true)
            {
                return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("All", "Users");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(CreateUserViewModel model)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == model.Username);

            if (user != null)
            {
                services.Update(model, user.Id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("All", "Users");
            }
        }
    }
}