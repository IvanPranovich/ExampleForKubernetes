using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExampleForKubernetes.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace ExampleForKubernetes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;

        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            // Quick example of DistributedCache usage
            const string cacheKey = "mykey";
            var value = _cache.GetString(cacheKey);
            var counter = string.IsNullOrWhiteSpace(value) ? 1 : Convert.ToInt32(value) + 1;
            _cache.SetString(cacheKey, counter.ToString());
            return View(counter.ToString());
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
