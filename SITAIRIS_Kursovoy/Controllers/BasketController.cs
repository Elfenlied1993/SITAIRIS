using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BSUIR.Web.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Address()
        {
            return View();
        }

        public IActionResult Confirm()
        {
            return View();
        }
    }
}