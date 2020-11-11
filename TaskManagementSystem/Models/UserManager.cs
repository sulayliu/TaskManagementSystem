using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TaskManagementSystem.Models
{
    public static class UserManager
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        public static List<ApplicationUser> ShowAllUsers()
        {
            return db.Users.ToList();
        }
        public static List<string> ShowAllRoles()
        {
            return db.Roles.Select(r => r.Name).ToList();
        }
        public static List<string> ShowAllRolesForAUser(string userId)
        {
            var roles = userManager.GetRoles(userId).ToList();
            return roles;
        }

        public static bool IsRoleExist(string roleName)
        {
            return roleManager.RoleExists(roleName);
        }

        public static bool UserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public static bool CreateRole(string roleName)
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

        public static bool DeleteRole(string roleName)
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
        public static IdentityResult AddUserToRole(string userId, string roleName)
        {
            return userManager.AddToRole(userId, roleName);
        }
        //public static bool AddUserToRole(string roleName, string userId)
        //{
        //    var userName = db.Users.Find(userId).UserName;
        //    //if (!CheckUserInRole(userName, roleName))
        //    //{
        //    return userManager.AddToRole(userId, roleName).Succeeded;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}

        //    roleName = roleName.ToLower();
        //    bool result = false;
        //    if (roleManager.RoleExists(roleName) && userManager.FindById(userId) != null)
        //    {
        //        result = userManager.IsInRole(userId, roleName);
        //        if (!result)
        //        {
        //            result = userManager.AddToRole(userId, roleName).Succeeded;
        //        }
        //    }
        //    return result;
        //}
        public static void SetSalary(string userId, double salary)
        {
            var user = db.Users.Find(userId);
            if (user != null)
            {
                user.Salary = salary;
            }
        }

        public static bool DeleteUserFromRole(string roleName, string userId)
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
