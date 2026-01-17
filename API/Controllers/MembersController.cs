using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.entities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class MembersController : BaseApiController
    {
        private readonly AppDbContext _context;

        public MembersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/members
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await _context.AppUsers.AsNoTracking().ToListAsync();
            return Ok(members);
        }

        // GET: api/members/id/{id}
        [HttpGet("id/{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await _context.AppUsers.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            return Ok(member);
        }
    }
}
