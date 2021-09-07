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
           //I get here by clicking on the green buttoms subcategories on each card item on the index on categories
           //the id for maincategories is passed and is used in the query to get all items with that foreing key
            IEnumerable<Subcategory> subKategoryList = _db.SubCategories.Where(t=> t.MainCategoryId == id);
            return View(subKategoryList);//this parameter is treated as the model for Razor synthax

            //below if for testing if the kontroller works and returns something
            //string todaysDate = DateTime.Now.ToShortDateString();
            //return Ok(todaysDate);
        }

        public IActionResult Details(int id)
        {
            Subcategory subKategoryObj = _db.SubCategories.Find(id);
            return View(subKategoryObj);//this parameter is treated as the model for Razor synthax

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
