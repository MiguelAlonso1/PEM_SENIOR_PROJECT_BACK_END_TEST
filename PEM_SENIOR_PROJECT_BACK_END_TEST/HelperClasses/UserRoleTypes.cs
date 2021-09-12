using Microsoft.AspNetCore.Mvc.Rendering;//for select list item
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.HelperClasses
{
    public static class UserRoleTypes
    {
        public static string Admin = "Admin";
        public static string MedicalStaff = "MedicalStaff";
        public static string Patient = "Doctor";

      public static  List<SelectListItem> GetRoleTypesForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem(UserRoleTypes.Admin, UserRoleTypes.Admin),
                new SelectListItem(UserRoleTypes.MedicalStaff, UserRoleTypes.MedicalStaff),
                new SelectListItem(UserRoleTypes.Patient, UserRoleTypes.Patient)
            };
        }
    }
}
