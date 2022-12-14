using BackEndAPI.Data;
using BackEndAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPI.Controllers
{
    public class BugController : BaseApiController
    {
        private readonly DataContext _context;

        public BugController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret txt";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            AppUser thing = _context.Users.Find(-1);

            if (thing == null)
                return NotFound();
            else
                return thing;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            AppUser thing = _context.Users.Find(-1);
            string thingToReturn = thing.ToString();

            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetbadRequest()
        {
            return BadRequest("This is a bad request!");
        }
    }
}