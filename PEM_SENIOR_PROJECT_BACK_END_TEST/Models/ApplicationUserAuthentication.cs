#region ::MOD LOG::
//M.A. 9-12-21 async synthax to POST and GET Register controller

#endregion

using Microsoft.AspNetCore.Identity;//for IdentityUser
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Models
{

    #region :::This class was added to extend the columns on AspNetUsers...:::
    /*
     This class was added to extend the columns on AspNetUsers
    AspNetUsers comes with a predefined set of columns. Below we added the Name column since it didn't have it
    it works cuz by inheriting from IdentityUser, it recognizes the new columns have been added.
    Also, in the PEM_APP_DBContext.cs class we pass this class. This auto-generates a new database migration
    we just need o push to have the extra columns added
     */

    #endregion
    public class ApplicationUserAuthentication:IdentityUser
    {
        public string Name { get; set; }
    }
}