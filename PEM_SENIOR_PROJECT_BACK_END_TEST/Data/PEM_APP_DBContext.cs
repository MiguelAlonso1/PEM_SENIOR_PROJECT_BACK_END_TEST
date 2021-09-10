/*
 * This class has to be created by user manually
 */
//had to install NuGet pkg after adding IdentityDbContext
//on line 16
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PEM_SENIOR_PROJECT_BACK_END_TEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Data
{
    //public class PEM_APP_DBContext: DbContext//this requires using Microsoft.EntityFrameworkCore;
    public class PEM_APP_DBContext : IdentityDbContext//this requires using Microsoft.EntityFrameworkCore;
    {
        public PEM_APP_DBContext(DbContextOptions<PEM_APP_DBContext> options) : base(options)
        {

        }

        public DbSet<MainCategory> MainCategories { get; set; }//this becomes a SQL server database table
        public DbSet<Subcategory> SubCategories { get; set; }//this becomes a SQL server database table
    }
}