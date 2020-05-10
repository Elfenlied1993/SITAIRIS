using System.Collections.Generic;
using System.Diagnostics;
using BSUIR.BL.Interfaces.Models;
using BSUIR.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BSUIR.Web.Controllers
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
            var coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates()
            {
                Lat = 48.855901,
                Lng = 2.349272
            });
            coordinates.Add(new Coordinates()
            {
                Lat = 52.520413,
                Lng = 13.402794
            });
            coordinates.Add(new Coordinates()
            {
                Lat = 41.907074,
                Lng = 12.498474
            });
            return View(coordinates);
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
