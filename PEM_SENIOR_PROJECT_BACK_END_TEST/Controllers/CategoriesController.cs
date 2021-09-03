/*These are the kategories for the PEM app
 * Medical, Surgical, Trauma, Toxicology, Foreign Ingestion, and Emergent Rashes
 
 */

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();

            //below if for testing if the kontroller works and returns something
            //string todaysDate = DateTime.Now.ToShortDateString();
            //return Ok(todaysDate);
        }

        public IActionResult Details(int id)
        {
           // return View();

            //below if for testing if the kontroller works and returns something
            //string todaysDate = DateTime.Now.ToShortDateString();
            return Ok($"You've enter: {id}");
        }
    }
}
