using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExampleForKubernetes.ModelBuilders;
using Microsoft.AspNetCore.Mvc;
using ExampleForKubernetes.Models;

namespace ExampleForKubernetes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeIndexModelBuilder _homeIndexModelBuilder;

        public HomeController(IHomeIndexModelBuilder homeIndexModelBuilder)
        {
            _homeIndexModelBuilder = homeIndexModelBuilder;
        }

        public IActionResult Index()
        {
            var model = _homeIndexModelBuilder.GetHomeIndexModel();
            return View(model);
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
    }
}
