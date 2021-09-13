#region ::MOD LOG::
//M.A. 9-12-21 async synthax to POST and GET Register controller
 
#endregion

using Microsoft.AspNetCore.Identity;//for User, signing, and role manager
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
        private readonly UserManager<ApplicationUserAuthentication> _userManager;
        private readonly SignInManager<ApplicationUserAuthentication> _signInManager;
        //role manager is still default so IdentityRole is being used
        private readonly RoleManager<IdentityRole> _roleManager;

        #region :::Inversion of control thru dependendy injection(more specifically, constructor injection):::
        // AccountController doens't manage nor create this atuhentication
        //stuff. It gets it from the outside
        /*
         * Inversion of control (IoC) is a design pattern in which the control flow of a program is inverted.
         * You can take advantage of the inversion of control pattern to decouple the components 
         * of your application, swap dependency implementations, mock dependencies, and make 
         * your application modular and testable.

         Dependency injection [is a subset] of the inversion of control principle. 
        In other words, dependency injection is just one way of implementing inversion of control. 
        You can also implement inversion of control using events, delegates, template pattern, factory method, 
        or service locator, for example.

        The inversion of control design pattern states that objects should not create objects 
        on which they depend to perform some activity. 
        Instead, they should get those objects from an outside service or a container. 
        The idea is analogous to the Hollywood principle that says, “Don’t call us, we’ll call you.”
        As an example, instead of the application calling the methods in a framework, the framework would call the 
        implementation that has been provided by the application.
        
        Inversion of control and dependency injection help you with automatic instantiation and 
        lifecycle management of your objects. ASP.NET Core includes a simple, 
        built-in inversion of control container with a limited set of features. 
        You can use this built-in IoC container if your needs are simple 
        or use a third-party container if you would like to leverage additional features.
         */
        #endregion
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
        //GET-Login
        public IActionResult Login()
        {
            return View();
        }
        #region ::Http is Web access. Web access benefits from asynchronous programming::
        // need to research this
        #endregion
        //POST-Login-always use POST for delete update and authentication for security reasons!
        [HttpPost]//need to research
        [ValidateAntiForgeryToken]//need to research
        public async Task<IActionResult> Login(LoginViewModel obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            //causes server-side validations
            if (!ModelState.IsValid) 
            {
                return View();//to show errors on form
            }
            var result = await _signInManager.PasswordSignInAsync(obj.Email,obj.Password,obj.RememberMe,false);
           
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Categories");
            }
            ModelState.AddModelError("", "Invalid User name or Password!");

            return View(obj);//return to show authentication error
        }

        //GET-Register
        public async Task<IActionResult> Register()
        {
            if(!_roleManager.RoleExistsAsync(UserRoleTypes.Admin).GetAwaiter().GetResult())
            {
                //apparently, once this app is deployed to Azure, creating roles and first user
                //can be done there and it's more elegant
               await _roleManager.CreateAsync(new IdentityRole(UserRoleTypes.Admin));
               await _roleManager.CreateAsync(new IdentityRole(UserRoleTypes.MedicalStaff));
               await _roleManager.CreateAsync(new IdentityRole(UserRoleTypes.Patient));
            }
            ViewBag.RoleTypes = UserRoleTypes.GetRoleTypesForDropDown();
            return View();
        }

        //POST-Register-always use POST for delete update and authentication for security reasons!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            //IdentityUser is tied to AspNetUsers table
            //activates server-side validations just in case client-side validations fail!
            if (!ModelState.IsValid)
            {
                return View();//goes back to view to show errors on form
            }
            //when creating the user, the password is not set with it. Instead, the password itself
            //is passed as its own parameter on callt to _userManager.CreateAsync on line 138
            var user = new ApplicationUserAuthentication//like creating an IdentityUser plus extra columns added by us
            {
                UserName = obj.Name,//in tutorial they put the email here instead. Cuz of privacy? Dunno
                Email = obj.Email,//from identity table
                Name = obj.Name,//from custom table added on ApplicationUserAuthentication.cs
            };
            //validate User Role before creating User
            var roleExists = _roleManager.Roles.FirstOrDefault(x => x.Name == obj.RoleName);
            if (roleExists == null)
            {
                ModelState.AddModelError("", obj.RoleName + " is not a valid User Role!");
                return View(obj);
            }

            //Insert new user into AspNetUsers table
            var result = await _userManager.CreateAsync(user,obj.Password);

            if (result.Succeeded) 
            {
                //insert user and role into AspNetUserRoles table
                await _userManager.AddToRoleAsync(user, obj.RoleName); 

                //after account is Created sign in user
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Categories");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(obj);//goes back to view to show list of errors on form
        }

        [HttpPost]
        public async Task<IActionResult> Logoff() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}