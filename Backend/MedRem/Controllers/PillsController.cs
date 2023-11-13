using MedRem.Models;
using MedRem.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MedRem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PillsController : Controller
    {
        private readonly MedRemDbContext _context;

        public PillsController(MedRemDbContext context)
        {
            _context = context;
        }

       

        [HttpGet("getPills/{userId}")]
        public async Task<ActionResult<IEnumerable<Pill>>> GetPills(int userId)
        {
            var schedules = await _context.Pills
                                          .Where(s => s.UserId == userId)
                                          .ToListAsync();
            if (schedules == null)
            {
                return NotFound();
            }
            return schedules;
        }
    }
}