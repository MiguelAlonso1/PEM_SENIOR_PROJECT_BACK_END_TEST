using Microsoft.AspNetCore.Mvc;
using PEM_SENIOR_PROJECT_BACK_END_TEST.Data;
using PEM_SENIOR_PROJECT_BACK_END_TEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Controllers
{
    public class SubcategoriesController : Controller
    { //dependency injection is done with variable below
        private readonly PEM_APP_DBContext _db;

        public SubcategoriesController(PEM_APP_DBContext options)
        {
            _db = options;
        }
        public IActionResult Index(int id)
        {
            IEnumerable<Subcategory> subKategoryList = _db.SubCategories.Where(t=> t.MainCategoryId == id);
            return View(subKategoryList);//this parameter is treated as the model for Razor synthax

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
            return View();//returns the HTML form
            //return Ok("yo!");
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MainCategory obj)//obj comes form the HTML form
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details");
            }

            _db.MainCategories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");//to the Index in Categories since this controller is in Categories
        }
    }
}
