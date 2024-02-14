using ArtShop.Data;
using ArtShop.Data.Entities;
using ArtShop.Services;
using ArtShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtShop.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _rps;

        public AppController(IMailService mailService, IDutchRepository rps)
        {
            _mailService = mailService;
            _rps = rps;
        }
        public IActionResult Index()
        {

            
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            
            ViewBag.Title = "Свяжитесь с нами";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("galifanov.renat@gmail.com", model.Subject, $"From: {model.Name}-{model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
            }
            
            ViewBag.Title = "Свяжитесь с нами";

            return View();
        }
        [HttpGet("about")]
        public IActionResult About()
        {
            ViewBag.Title = "О нас";
            return View();
        }

        [HttpGet("shop")]
        [Authorize]
        public IActionResult Shop()
        {

            IEnumerable<Product> products = _rps.GetAllProducts();
            return View(products);
        }
    }
}
