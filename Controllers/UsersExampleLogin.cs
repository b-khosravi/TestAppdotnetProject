using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestApp.Controllers
 {
     [ApiController]
     [Route("[controller]")]
      public class UsersExampleLoginController : ControllerBase
       {
           [HttpGet]
           [Route("GetUsersExampleLoginData")]
          // [Authorize(Policy =Policies.UsersExampleLogin)]
            public IActionResult GetUserExampleLoginData()
            {
                return Ok("This is a response from user method");
            }
    [HttpGet]
    [Route("GetAdminData")]
    [Authorize(Policy = Policies.Admin)]
    public IActionResult GetAdminData()
     {
         return Ok("This is a response from Admin method");
     }
    }
  }