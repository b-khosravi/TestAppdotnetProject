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
using Microsoft.AspNetCore.Authorization;

namespace TestApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Books/GetBooks
        [HttpGet]
        [Authorize(Policy="RequireUserRole")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(int pageNumber=1,int pageSize=3)
        {

            int totalRecords= _context.Books.Count();
           int skip=(pageNumber-1)* pageSize;

           var books =  _context.Books
                .OrderBy(x => x.BookId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

                return Ok(new PageResult<Book>(books,pageNumber,pageSize,totalRecords));

          ///  using LibraryTestApiWebEntites entites = new LibraryTestApiWebEntites();
           // return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int bookid)
        {
            var book = await _context.Books.FindAsync(bookid);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/PutBook
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int bookid, Book book)
        {
            //using (Book  entity = new Book)
            if (bookid != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(bookid))
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

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [HttpPost]

        [Authorize(Policy="ReuireUserRole")]

        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { bookid = book.BookId }, book);

        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int bookid)
        {
            var book = await _context.Books.FindAsync(bookid);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int bookid)
        {
            return _context.Books.Any(e => e.BookId == bookid);
        }
    }
}
