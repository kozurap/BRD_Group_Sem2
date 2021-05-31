using BRD_Sem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BRD_Sem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContentPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserPage()
        {
            return View();
        }
        public IActionResult AdminPage()
        {
            return View();
        }
        public IActionResult TVShows()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult Movies()
        {
            return View();
        }
        public IActionResult Sports()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Songs()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
