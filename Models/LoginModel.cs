using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;  
  
namespace TestApp.Authentication  
{  
    public class LoginModel  
    {  
        public string UserName { get; set; }  
  
        public string Password { get; set; }  
    }  
}