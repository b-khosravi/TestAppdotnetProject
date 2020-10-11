using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Authentication;

namespace TestApp.Models
{
    public class Shelf
    {
        public Shelf()
        {
            BooksShelfs = new ObservableCollection<BooksShelf>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<BooksShelf> BooksShelfs { get; set; }
    }
}