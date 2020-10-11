using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class Book
    {
        public Book()
        {
            BooksShelfs = new ObservableCollection<BooksShelf>();
        }

        // [Key]
        public int BookId { get; set; }

        //  [InverseProperty("Book")]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }

        public ICollection<BooksShelf> BooksShelfs { get; set; }

    }
}
