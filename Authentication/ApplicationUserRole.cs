using Microsoft.AspNetCore.Identity;
//using  Microsoft.SqlServer.Management.Smo 

namespace TestApp.Authentication
{
    // public class ApplicationUserRole
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        //  public string Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }

    }
}
