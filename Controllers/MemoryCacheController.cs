using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
using System.Runtime.Caching;
using TestApp.Authentication;
using System.Linq;
using System.Text;
//using System.Runtime.Caching.CacheItemPriority;

namespace TestApp.Controllers
{
    [Route ("api/[controller]")]
     [ApiController]

     public class MemoryCacheController:ControllerBase
     {
        // private readonly IMemoryCache memoryCache;

        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context;

        public MemoryCacheController(IMemoryCache memoryCache, AppDbContext context)
         {
             _cache=memoryCache;
             _context=context;
         }

           [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserList(string userName )
        {
            var cacheKey =userName.ToLower();
 
             IEnumerable<UserDTO> userList;

            if (!_cache.TryGetValue(cacheKey, out  userList)) 
            {
              userList = _cache.Get<IEnumerable<UserDTO>>(cacheKey);

              var cacheExpirationOptions =
                    new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(6),
                       // Priority = CacheItemPriority.Normal,
                        SlidingExpiration= TimeSpan.FromMinutes(5)
                    };
                _cache.Set(cacheKey, userList, cacheExpirationOptions);
            }
            return Ok(userList);

        }

      
        /*  public IActionResult CacheTryGetValueSet()  
{ 
        DateTime currentTime;
        //var users = _cacheProvider.GetFromCache<IEnumerable<User>>(CacheKeys.Entry);

    if (!_cache.TryGetValue (CacheKeys.Entry, out currentTime)) 

       {

        currentTime = DateTime.Now;
        var cacheEntryOptions= new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromSeconds (30));
        //Priority=CacheItemPriority.Normal,
       // AbsoluteExpiration=DateTime.Now.AddSeconds(10)


        _cache.Set (CacheKeys.Entry, currentTime, cacheEntryOptions);
       }
    return Ok(currentTime);*/

        /*  var cacheKey = "TheTime";
         DateTime currentTime;
     if (_cache.TryGetValue(cacheKey, out currentTime))
     {
         return Ok("Fetched from cache : " + currentTime.ToString());
     }
     else
     {
         currentTime = DateTime.UtcNow;
         _cache.Set(cacheKey, currentTime);
         return Ok("Added to cache : " + currentTime);
     }
         */


    } 

        
    }
 // }
 // }

  
