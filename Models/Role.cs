
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TestApp.Models
{
public class Role
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<UserDTO> Users { get; set; }

    }
}