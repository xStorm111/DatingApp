using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        //IEnumerable is for list/arrays, not single objects
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = _context.Users.ToListAsync();

            return await users;
        }

        // api/users/3
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {

            //in the course Niel says we can reduce this to only return _context.Users.Find(id);
            //i still prefer this way because its a lot easier to debug if we need too
            var user = _context.Users.FindAsync(id);
            return await user;
        }
    }
}
