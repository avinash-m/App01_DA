using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using API.Entities;
using API.Data;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class UsersController: BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            return await _context.Users.ToListAsync();
        }

        [Authorize]
        // api/users/3
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            return await _context.Users.FindAsync(id);           
        }
    }
}