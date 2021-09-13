/*
 * Modlog
 Startup acts as the Dependency Injection Kontainer DI
 9/10 change database string connection from DefaultConnection to PEM_DatabaseConnection (line 37)
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;//this allows me to call UseSqlSever when configuring the database
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PEM_SENIOR_PROJECT_BACK_END_TEST.Data;//so I can use my PEM DBContext class
using PEM_SENIOR_PROJECT_BACK_END_TEST.Models;//for ApplicationUserAuthentication
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // This is the Dependency Injection kontainer- Miguel Alonso comment
        public void ConfigureServices(IServiceCollection services)
        {
            //below the sql server connection for PEM is setup
            services.AddDbContext<PEM_APP_DBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("PEM_DatabaseConnection")));
            services.AddControllersWithViews();

            //Registering Identity login and registration service
            services.AddIdentity<ApplicationUserAuthentication, IdentityRole>().AddEntityFrameworkStores<PEM_APP_DBContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {//Home refers to the kontroller not the view.Error to the Aktion result Miguel Alonso 
                app.UseExceptionHandler("/Home/Error");//Kontroller/Aktion
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();//uses wwwroot folder from project

            app.UseRouting();//since this project is MVC the routing used is MVC routing
            //if the project was Razor Pages, that would've been the routing used, and so on and so forth

            //userAuthentication is needed. If this was missing, the navigation bar
            //logic in _LoginPartialView.cshtml wouldn't work
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               //This means, that by default, if the kontroller is home, by default the index.
               //Also, is the home is ommited, the default is still index
               //the question mark next to id, means id is optional
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Categories}/{action=Index}/{id?}");
            });
        }
    }
}
