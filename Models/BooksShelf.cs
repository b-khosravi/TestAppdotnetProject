using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class BooksShelf
    {

        public int Id { get; set; }
        public int BookId { get; set; }
        public int ShelfId { get; set; }

        public Book Book { get; set; }
        public Shelf Shelf { get; set; }

    }
}
