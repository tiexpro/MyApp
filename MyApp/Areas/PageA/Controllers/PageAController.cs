using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyTest;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Areas.PageA.Controllers
{
    public class PageAController : Controller
    {
        private IDependencyService _IDependencyService;

        public PageAController(IDependencyService dependencyService)
        {
            _IDependencyService = dependencyService;
        }

        public IActionResult Index()
        {
            ViewBag.TestData = _IDependencyService.ServiceTest();
            return View();
        }
    }
}