using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.IO;
using System.Web;
using LAB4_1203819_2530019.Models;


namespace LAB4_1203819_2530019.Controllers
{
    public class HomeController : Controller
    {
        private readonly Models.Data.Singleton F = Models.Data.Singleton.Instance;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult OptionList(int id)
        {
            if (F.Developer == null)
            {
                return RedirectToAction("DeveloperName", "Tareas");
            }
            return RedirectToAction("DeveloperChoose", "Tareas");
        }
        public IActionResult OptionList2(int id)
        {
            return RedirectToAction("login", "Tareas");
        }
    }
}
