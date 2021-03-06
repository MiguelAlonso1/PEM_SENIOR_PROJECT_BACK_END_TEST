#region ::MOD LOG::
/*This class should implement ViewModels (like services classes)
 decided to not implemented in this project to keep it simple for studying purposes
 */
#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;//for SelectListItem for the drop-down-list for the Foreingkey
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
            #region ::I get here by clicking on..::
            //I get here by clicking on the green buttoms subcategories on each card item on the index on categories
            //the id for maincategories is passed and is used in the query to get all items with that foreing key
            #endregion
            IEnumerable<Subcategory> subKategoryList = _db.SubCategories.Where(t=> t.MainCategoryId == id);
            string mainCategoryName = _db.MainCategories.Find(id).CategoryName;
            ViewBag.MainCategoryName = mainCategoryName;
            return View(subKategoryList);//this parameter is treated as the model for Razor synthax

            #region ::Below if for testing if::
            //below if for testing if the kontroller works and returns something
            //string todaysDate = DateTime.Now.ToShortDateString();
            //return Ok(todaysDate);
            #endregion
        }

        public IActionResult Details(int id)
        {
            Subcategory subKategoryObj = _db.SubCategories.Find(id);
            return View(subKategoryObj);//this parameter is treated as the model for Razor synthax

            #region ::Below if for testing if::
            //below if for testing if the kontroller works and returns something
            //string todaysDate = DateTime.Now.ToShortDateString();
            //return Ok($"You've enter: {id}");
            #endregion
        }
        //GET-Create
        public IActionResult Create()
        {
            //Load all current Main kategories from database. this is for html drop-down-list
            IEnumerable<SelectListItem> mainKategoryNameValues =
                _db.MainCategories.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }
                                         );
            //ViewBag is automatically passed to the view
            ViewBag.MainKategoryListOfNames = mainKategoryNameValues;
            return View();//returns the HTML form
            //return Ok("yo!");
        }
        #region ::Http is Web access. Web access benefits from asynchronous programming::
        // need to research this
        #endregion
        //POST-Create-always use POST for delete update and authentication for security reasons!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subcategory obj)//obj comes form the HTML form
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _db.SubCategories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", new { Id= obj.MainCategoryId});//to the Index in Categories since this controller is in Categories
        }


        //GET-Update
        public IActionResult Update(int? id)//obj comes form the HTML form
        {
            if (id == null || id == 0)//in Sql table, all IDs start from 1
            {
                return NotFound();
            }
            var obj = _db.SubCategories.Find(id);//Find matches on Primary Key only

            if (obj == null)
            {
                return NotFound();
            }

            MainCategory mainKategoryList = _db.MainCategories.FirstOrDefault (x => x.Id == obj.MainCategoryId);
            if (mainKategoryList == null)
            {
                return NotFound();
            }

            SelectListItem mainKategorySelectList =
                 new SelectListItem
                 {
                     Text = mainKategoryList.CategoryName,
                     Value = mainKategoryList.Id.ToString()
                 };

            #region ::The below list is needed since...::

            /*The below list is needed since the HTML select list asp-items control
             wants an object that resolves as an IEnumerable so it can iterate throgh it
            to render the option HMTL tags for some reason creating a IEnumerable variable
            to assign it the list item didn't work. Maybe cuz it's an abstract object
            The list below resolves as an IEnumerable so this worked
             */
            #endregion
            List<SelectListItem> sl = new List<SelectListItem>();
            sl.Add(mainKategorySelectList);

            //ViewBag is automatically passed to the view
            ViewBag.MainKategorySelectList = sl;
            return View(obj);
        }
        #region ::Http is Web access. Web access benefits from asynchronous programming::
        // need to research this
        #endregion
        //POST-Update-always use POST for delete update and authentication for security reasons!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Subcategory obj)//obj comes form the HTML form
        {
            //if (obj == null)
            //{
            //    return NotFound();
            //}
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _db.SubCategories.Update(obj);
            _db.SaveChanges();

            #region ::The second parameter is just part...::
            /*
            the second parameter is just part of an object called route values. Id is a part of it
            would look something like this
            return RedirectToAction( "Main", new RouteValueDictionary( 
            new { controller = controllerName, action = "Main", Id = Id } ) );
            */
            #endregion

            return RedirectToAction("Index", new { id = obj.MainCategoryId});//to the Index in Subcategories since this controller is in Categories
        }

        //GET-Delete
        public IActionResult Delete(int? id)//obj comes form the HTML form
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.SubCategories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            MainCategory mainKategoryList =
                _db.MainCategories.FirstOrDefault(x => x.Id == obj.MainCategoryId);
            if (mainKategoryList == null)
            {
                return NotFound();
            }

            SelectListItem mainKategorySelectList =
                 new SelectListItem
                 {
                     Text = mainKategoryList.CategoryName,
                     Value = mainKategoryList.Id.ToString()
                 };

            #region ::The below list is needed since...::
            /*The below list is needed since the HTML select list asp-items control
            wants an object that resolves as an IEnumerable so it can iterate throgh it
           to render the option HMTL tags for some reason creating a IEnumerable variable
           to assign it the list item didn't work. Maybe cuz it's an abstract object
           The list below resolves as an IEnumerable so this worked
            */
            #endregion
            List<SelectListItem> sl = new List<SelectListItem>();
            sl.Add(mainKategorySelectList);

            //ViewBag is automatically passed to the view
            ViewBag.MainKategorySelectList = sl;

            return View(obj);
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
            var obj = _db.SubCategories.Find(id);
            
            if (obj == null)
            {
                return NotFound();
            }
            //save foreing key ID before deleting so we can redirect back to index for the currenty subcategory index
            int primaryKeyID = obj.MainCategoryId;

            _db.SubCategories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = primaryKeyID });//to the Index in Categories since this controller is in Categories
        }
    }
}