using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
           _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret text";
        }
        [Authorize]
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);
            if (thing == null) return NotFound();
            return Ok(thing);
        }

        [Authorize]
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
                var thing = _context.Users.Find(-1);
                var thingToReturn = thing.ToString();
                return thingToReturn;  
        }
        [Authorize]
        [HttpGet("bad-request")]
        public ActionResult<AppUser> GetBadRequest()
        {
            return BadRequest("Bad Request");
        }
    }
}
