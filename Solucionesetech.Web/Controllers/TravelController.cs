using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Solucionesetech.Web.Models;

namespace Solucionesetech.Web.Controllers
{
    public class TravelController : Controller
    {
        private IConfiguration _configuration;
        public TravelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.API_URL = _configuration["Solucionesetech:API.URL"];
            return View();
        }
    }
}
