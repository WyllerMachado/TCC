using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using ClassLogger.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClassLogger.Extensions
{
    public static class IdentityExtensions
    {
        public static ApplicationUser GetApplicationUser(this IIdentity identity)
        {
            if (!identity.IsAuthenticated)
                return null;

            using (var db = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                return userManager.FindByName(identity.Name);
            }
        }
    }
}