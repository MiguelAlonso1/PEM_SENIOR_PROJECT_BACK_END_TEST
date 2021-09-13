using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Controllers
{
    public class Appoinment : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
