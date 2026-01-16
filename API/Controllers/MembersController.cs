using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MembersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/members
        [HttpGet]
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
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
    }
}
