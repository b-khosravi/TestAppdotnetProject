
/*using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Authentication
{

public class AccessControlService
{
        private AppDbContext db;

        public AccessControlService()
        {
            db = new AppDbContext();
        }

        public IEnumerable<Permission> GetUserPermissions(string userId)
        {
            var userRoles = this.GetUserRoles(userId);
            var userPermissions = new List<Permission>();
            foreach (var userRole in userRoles)
            {
                foreach (var permission in userRole.Permissions)
                {
                    // prevent duplicates
                    if (!userPermissions.Contains(permission))
                        userPermissions.Add(permission);
                }
            }
            return userPermissions;
        }

        internal object GetPermissions(object p)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Role> GetUserRoles(string userId)
        {
            return db.Users.FirstOrDefault(x => x.UserId == userId).Roles.ToList();
        }

        public bool HasPermission(string userId, string area, string control)
        {
            var found = false;
            var userPermissions = this.GetUserPermissions(userId);
            var permission = userPermissions.FirstOrDefault(x => x.Area == area && x.Control == control);
            if (permission != null)
                found = true;
            return found;
        }

}
*/