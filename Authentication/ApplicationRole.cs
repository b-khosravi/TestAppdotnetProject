using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
//using  Microsoft.SqlServer.Management.Smo 

namespace TestApp.Authentication
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            
        }
     // public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
        // public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
