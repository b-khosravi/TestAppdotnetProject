using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
//using Microsoft.EntityFrameworkCore.Tools;
using Microsoft.Extensions.Hosting;



namespace TestApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users/GetUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(int pageNumber=1 , int pageSize=2)
        {
           //string s="hello";
         //  int h=int.Parse(s);
           int totalRecords= _context.Users.Count();
           int skip=(pageNumber-1)* pageSize;

           var users = _context.Users
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(pageSize)
           .Select(x => new UserDTO{
                Name = x.UserName,
                UserId=x.Id,
                Username=x.UserName
            }).ToList();

             
                return Ok(new PageResult<UserDTO>(users,pageNumber,pageSize,totalRecords));
                

            /*var users = await _context.Users
                .Include(x => x.Shelfs) 
                .Select(x => new User
                {
                    UserId = x.UserId,
                    Name = x.Name,
                    Username = x.Username,
                    Password = x.Password,
                    Shelfs = x.Shelfs
                        .Select(t => new Shelf
                         {
                            Id = t.Id,
                            UserId = t.UserId,
                            Title = t.Title,
                            BooksShelfs=t.BooksShelfs.Select(m=>new BooksShelf{Id=m.Id,BookId=m.BookId,ShelfId=m.ShelfId}).ToList()

                        }) .ToList()

                }).ToListAsync();

            return users;*/

        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string userid)
        {
            var user = await _context.Users.FindAsync(userid);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO{
                Name = user.UserName,
                UserId=user.Id,
                Username=user.UserName
            };
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string userid, UserDTO user)
        {
            if (userid != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userid))
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

        // POST: api/Users
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO user)
        {
            // _context.Users.Add(user);
               
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { userid = user.UserId }, user);
            

            //  return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete(/*"{id}"*/)]
        public async Task<ActionResult<UserDTO>> DeleteUser(int userid)
        {
            var user = await _context.Users.FindAsync(userid);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new UserDTO{
                Name = user.UserName,
                UserId=user.Id,
                Username=user.UserName
            };
        }

        private bool UserExists(string userid)
        {
            return false;// _context.Users.Any(e => e.UserId == userid);
        }
    }
}
