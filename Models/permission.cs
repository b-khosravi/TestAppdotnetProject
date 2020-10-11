
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TestApp.Models
{
public class Permission
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public string Control { get; set; }
      //  public virtual ICollection<Role> Roles { get; set; }
    }
}