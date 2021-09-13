#region ::MOD LOG::
/*These are the kategories for the PEM app
 * Medical, Surgical, Trauma, Toxicology, Foreign Ingestion, and Emergent Rashes
 */
#endregion

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

        public IActionResult Modal()
        {
            return View();
        }
        public IActionResult Index()
        {
            IEnumerable<MainCategory> kategoryList = _db.MainCategories;
            IEnumerable<Subcategory> subcategoryList = _db.SubCategories;
            ViewBag.subCatList = subcategoryList;
            return View(kategoryList);//this parameter is treated as the model for Razor synthax

            #region ::below if for testing if the kontroller works and returns something::
            //string todaysDate = DateTime.Now.ToShortDateString();
            //return Ok(todaysDate);
            #endregion
        }

        //GET-Create
        public IActionResult Create()
        {
            return View();//returns the HTML form
            //return Ok("yo!");
        }
        #region ::Http is Web access. Web access benefits from asynchronous programming::
        // need to research this
        #endregion
        //POST-Create-always use POST for delete update and authentication for security reasons!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MainCategory obj)//obj comes form the HTML form
        {
           if (!ModelState.IsValid)
            {
               return View(obj);//basically returns to the same page with the same object to display the error msgs
            }

            _db.MainCategories.Add(obj);
            _db.SaveChanges();
           return RedirectToAction("Index");//to the Index in Categories since this controller is in Categories
        }

        //GET-Delete
        public IActionResult Delete(int? id)//obj comes form the HTML form
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.MainCategories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            // return View(obj);

            _db.MainCategories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //POST-Delete-always use POST for delete update and authentication for security reasons!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)//obj comes form the HTML form
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.MainCategories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.MainCategories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");//to the Index in Categories since this controller is in Categories
        }

        //GET-Update
        public IActionResult Update(int? id)//obj comes form the HTML form
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.MainCategories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST-Update-always use POST for delete update and authentication for security reasons!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(MainCategory obj)//obj comes form the HTML form
        {
            if (obj == null)
            {
                return NotFound();
            }
            //below is needed to validate errors
            if (!ModelState.IsValid)
            {
                return View(obj);//basically returns to the same page with the same object to display the error msgs
            }

            _db.MainCategories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");//to the Index in Categories since this controller is in Categories
        }

    }
}