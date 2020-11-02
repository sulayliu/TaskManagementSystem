using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class UserManager
    {
        private protected UserManager<IdentityUser> userManager;
        private protected RoleManager<IdentityRole> roleManager;
        public UserManager()
        {
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        public bool isRoleExist(string roleName)
        {
            return roleManager.RoleExists(roleName);
        }

        public bool createRole(string roleName)
        {
            roleName = roleName.ToLower();
            bool result = false;
            if (roleManager.RoleExists(roleName))
            {
                return true;
            }
            else
            {
                result = roleManager.Create(new IdentityRole(roleName)).Succeeded;
            }

            return result;
        }

        public bool deleteRole(string roleName)
        {
            roleName = roleName.ToLower();
            bool result = false;
            if (roleManager.RoleExists(roleName))
            {
                var users = roleManager.FindByName(roleName).Users.ToList().Select(x => x.UserId).ToList();
                users.ForEach(uId =>
                {
                    userManager.RemoveFromRole(uId, roleName);
                });
                result = roleManager.Delete(roleManager.FindByName(roleName)).Succeeded;
            }
            return result;
        }

        public bool addUserToRole(string roleName, string userId)
        {
            roleName = roleName.ToLower();
            bool result = false;
            if (roleManager.RoleExists(roleName) && userManager.FindById(userId) != null)
            {
                result = userManager.IsInRole(userId, roleName);
                if (!result)
                {
                    result = userManager.AddToRole(userId, roleName).Succeeded;
                }
            }
            return result;
        }

        public bool deleteUserFromRole(string roleName, string userId)
        {
            roleName = roleName.ToLower();
            bool result = false;
            if (roleManager.RoleExists(roleName) && userManager.FindById(userId) != null)
            {
                if (userManager.IsInRole(userId, roleName))
                {
                    result = userManager.RemoveFromRole(userId, roleName).Succeeded;
                }
            }
            return result;
        }
    }
}
