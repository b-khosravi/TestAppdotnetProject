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
    public class ShelfsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShelfsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Shelfs/GetShelfs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelf>>> GetShelfs()
        {
        return await _context.Shelfs.ToListAsync();
        /* var shelfs= await _context.Shelfs
           .Where(y=>y.UserId==userid)
          .Select(y=>new Shelf
          {
            Id=y.Id,
            UserId=y.UserId,
            Title=y.Title
          }).ToListAsync();
         return shelfs;
*/
           // 
           /* var shelfs = await _context.Shelfs
                  .Include( y=> y.BooksShelfs)
                  .Select(y=>new Shelf
                  {
                      Id=y.Id,
                      UserId=y.UserId,
                      Title=y.Title,
                      BooksShelfs=y.BooksShelfs
                      .Select(b => new BooksShelf
                      {
                          Id=b.Id,
                          BookId=b.BookId,
                          ShelfId=b.ShelfId
                      
                  }).ToList()
        })
        .ToListAsync();
        return shelfs;
        */
                  



        }

        // GET: api/Shelfs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shelf>> GetByUserId(int id)
        {
            var shelf = await _context.Shelfs.FindAsync(id);

            if (shelf == null)
            {
                return NotFound();
            }
       
            return shelf;
        }

        // PUT: api/Shelfs/PutShelf
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelf(int id, Shelf shelf)
        {
            if (id != shelf.Id)
            {
                return BadRequest();
            }

            _context.Entry(shelf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShelfExists(id))
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

        // POST: api/Shelfs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Shelf>> PostShelf(Shelf shelf)
        {
            _context.Shelfs.Add(shelf);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetShelfs), new { id = shelf.Id }, shelf);

            // return CreatedAtAction("GetShelf", new { id = shelf.Id }, shelf);
        }

        // DELETE: api/Shelfs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shelf>> DeleteShelf(int id)
        {
            var shelf = await _context.Shelfs.FindAsync(id);
            if (shelf == null)
            {
                return NotFound();
            }

            _context.Shelfs.Remove(shelf);
            await _context.SaveChangesAsync();

            return shelf;
        }

        private bool ShelfExists(int id)
        {
            return _context.Shelfs.Any(e => e.Id == id);
        }
    }
}
