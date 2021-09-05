/*These are the kategories for the PEM app
 * Medical, Surgical, Trauma, Toxicology, Foreign Ingestion, and Emergent Rashes
 
 */

using Microsoft.AspNetCore.Mvc;
using PEM_SENIOR_PROJECT_BACK_END_TEST.Data;//to use db context class
using PEM_SENIOR_PROJECT_BACK_END_TEST.Models;//to use MainCategory (which is the table on SqlServer)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Controllers
{
    public class CategoriesController : Controller
    {
        //dependency injection is done with variable below
        private readonly PEM_APP_DBContext _db;

        public CategoriesController(PEM_APP_DBContext options)
        {
            _db = options;
        }
        public IActionResult Index()
        {
            IEnumerable<MainCategory> kategoryList = _db.MainCategories;
            return View(kategoryList);//this parameter is treated as the model for Razor synthax

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
        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MainCategory obj)
        {
            _db.MainCategories.Add(obj);
            _db.SaveChanges();
           return RedirectToAction("Index");//to the Index in Categories since this controller is in Categories
        }
    }
}
