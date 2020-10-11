using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
//using system.Net.Http;
using System.Data;

namespace TestApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksShelfsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksShelfsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BooksShelfs/GetBooksShelfs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksShelf>>> GetBooksShelfs()
        {
            return await _context.BooksShelfs.ToListAsync();
        }

        // GET: api/BooksShelfs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BooksShelf>> GetBooksShelf(int id)
        {
         
            var booksshelf = await _context.BooksShelfs.FindAsync(id);

            if (booksshelf == null)
            {
                return NotFound();
            }

            return booksshelf;
        }

        // PUT: api/Books/PutBook
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooksShelf(int id, BooksShelf booksshelf)
        {
            if (id != booksshelf.Id)
            {
                return BadRequest();
            }

            _context.Entry(booksshelf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksShelfExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BooksShelfs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BooksShelf>> PostBooksShelf(BooksShelf booksshelf)
        {
            _context.BooksShelfs.Add(booksshelf);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooksShelf), new { id = booksshelf.Id }, booksshelf);

            // return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BooksShelf>> DeleteBooksShelf(int id)
        {
            var booksshelf = await _context.BooksShelfs.FindAsync(id);
            if (booksshelf == null)
            {
                return NotFound();
            }

            _context.BooksShelfs.Remove(booksshelf);
            await _context.SaveChangesAsync();

            return booksshelf;
        }

        private bool BooksShelfExists(int id)
        {
            return _context.BooksShelfs.Any(e => e.Id == id);
        }
    }
}
