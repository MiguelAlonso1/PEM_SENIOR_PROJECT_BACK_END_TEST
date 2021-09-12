using Microsoft.AspNetCore.Identity;//for User, signin, and role manager
using Microsoft.AspNetCore.Mvc;
using PEM_SENIOR_PROJECT_BACK_END_TEST.Data;//for PEM_APP_DBContext
using PEM_SENIOR_PROJECT_BACK_END_TEST.HelperClasses;//for UserRoleTypes for ViewBag
using PEM_SENIOR_PROJECT_BACK_END_TEST.Models;//for ApplicationUserAuthentication
using PEM_SENIOR_PROJECT_BACK_END_TEST.Models.ViewModels;//for LoginViewModel
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Controllers
{
    public class AccountController : Controller
    {
        private readonly PEM_APP_DBContext _db;
        UserManager<ApplicationUserAuthentication> _userManager;
        SignInManager<ApplicationUserAuthentication> _signInManager;
        RoleManager<IdentityRole> _roleManager;


        public AccountController(PEM_APP_DBContext db, 
                                UserManager<ApplicationUserAuthentication> userManager,
                                SignInManager<ApplicationUserAuthentication> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        //GET
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel obj)
        {
            if (obj == null)
            { 
                return BadRequest();
            }
            if (!ModelState.IsValid) 
            {
                return View();//to show errors on form
            }
            return RedirectToAction("Index", "Categories");
        }

        //GET
        public async Task<IActionResult> Register()
        {
            if(!_roleManager.RoleExistsAsync(UserRoleTypes.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoleTypes.Admin));
               await _roleManager.CreateAsync(new IdentityRole(UserRoleTypes.MedicalStaff));
               await _roleManager.CreateAsync(new IdentityRole(UserRoleTypes.Patient));
            }
            ViewBag.RoleTypes = UserRoleTypes.GetRoleTypesForDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            //below if activates server-side validations just in case client-side validations fail!
            if (!ModelState.IsValid)
            {
                return View();//to show errors on form
            }
            var user = new ApplicationUserAuthentication
            {
                UserName = obj.Email,
                Email = obj.Email,//from identity table
                Name = obj.Name//form custom table
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(user, obj.RoleName);
                //after account is Created sign in user
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Categories");
            }
            return View();
        }
    }
}
