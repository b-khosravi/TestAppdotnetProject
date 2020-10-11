using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class UserDTO
    {
        public UserDTO()
        {
            Shelfs = new ObservableCollection<Shelf>();
        }
        // [Required]
        // [Key]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

      // ساخت ارتباط روی کلاس .... یک کاربر چندین قفسه ممکن است داشته باشد.
      
        public ICollection<Shelf> Shelfs { get; set; }


        //  public Book Book {get; set;}
        //       //[ForeignKey("BookId")]

        //  public int BookId{get;set;}

        // public int CurrentBookId{get;set;}
        //public Book CurrentBook{get;set;}


    }
}
