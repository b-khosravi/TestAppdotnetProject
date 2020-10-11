using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
//using  Microsoft.SqlServer.Management.Smo 

namespace TestApp.Authentication
{
    public class ApplicationUser : IdentityUser<string>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

//add baade register and login
  /*   public virtual ICollection<Role> Roles { get; set; }    
}

    }
    */


