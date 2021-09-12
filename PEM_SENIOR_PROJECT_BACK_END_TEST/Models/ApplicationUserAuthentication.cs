using Microsoft.AspNetCore.Identity;//for IdentityUser
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Models
{
    public class ApplicationUserAuthentication:IdentityUser
    {
        public string Name { get; set; }
    }
}
