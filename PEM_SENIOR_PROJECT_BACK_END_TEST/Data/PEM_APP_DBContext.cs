#region ::MOD LOG::
/*
 * This class has to be created by user manually
 */
//had to install NuGet pkg after adding IdentityDbContext
//on line 16

#endregion

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PEM_SENIOR_PROJECT_BACK_END_TEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Data
{
    #region ::public class PEM_APP_DBContext: DbContext...::
    //public class PEM_APP_DBContext: DbContext//this requires using Microsoft.EntityFrameworkCore;
    //IdentityUser was the default. ApplicationUserAuthentication is extending IdentityUser
    //to add more columns to the AspNetUsers table
    //The <ApplicationUserAuthentication> added below to IdentityDbContext, by default before it was
    //IdentityUser but <IdentityUser> was not explicitely written in line 21 before since it was implicitely defined
    //This modification below, along with the new ApplicationUserAthentication class to add columns,
    //automatically cause changes to the database migration to add new columns and stuff
    #endregion
    public class PEM_APP_DBContext : IdentityDbContext<ApplicationUserAuthentication>
    {
        public PEM_APP_DBContext(DbContextOptions<PEM_APP_DBContext> options) : base(options)
        {

        }

        public DbSet<MainCategory> MainCategories { get; set; }//this becomes a SQL server database table
        public DbSet<Subcategory> SubCategories { get; set; }//this becomes a SQL server database table
    }
}