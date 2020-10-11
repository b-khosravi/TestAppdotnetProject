using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace TestApp.Models
{
    public class RegisterModel
    {
        public string Id{get;set;}

      //  [Required(ErrorMessage = "User Name is required")]  
      public string FirstName{get;set;}
      public string LastName{get; set;}
      
        public string UserName{get;set;}
    
      //   [DataType(DataType.Password)]
      //  [Required(ErrorMessage = "Password is required")]  
      
        public string Password{get;set;}

      //   [EmailAddress]  
      //  [Required(ErrorMessage = "Email is required")]

        public string EmailAddress{ get;set;}

    
      //  public bool RememberMe{get; set;}
    }

}