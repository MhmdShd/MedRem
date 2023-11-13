using MedRem.Models;
using MedRem.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MedRem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly MedRemDbContext _context;

        public ScheduleController(MedRemDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateSchedule([FromBody] ScheduleDto scheduleDto, int userId = 1)
        {
            if (scheduleDto.PillId.HasValue && _context.Pills.Any(p => p.Id == scheduleDto.PillId.Value))
            {
                var schedule = new PillSchedule
                {
                    UserId = scheduleDto.UserId,
                    PillId = scheduleDto.PillId.Value,
                    Frequency = scheduleDto.Frequency,
                    Dosage = scheduleDto.Dosage,
                    FinishDate = scheduleDto.FinishDate,
                    StartDate = DateTime.Now.ToString(),
                    Time = scheduleDto.Time,
                };

                _context.PillSchedules.Add(schedule);
                await _context.SaveChangesAsync();

            }
            else if (!scheduleDto.PillId.HasValue && !string.IsNullOrEmpty(scheduleDto.PillName))
            {
                var pill = new Pill
                {
                    Name = scheduleDto.PillName,
                    UserId = userId,
                    Shape = scheduleDto.PillShape,
                    Description = scheduleDto.Description,
                    Type = scheduleDto.PillType,
                    Image = scheduleDto.Image,
                    Color = scheduleDto.PillColor,

                };

                _context.Pills.Add(pill);
                await _context.SaveChangesAsync();

                var schedule = new PillSchedule
                {
                    UserId = scheduleDto.UserId,
                    PillId = pill.Id,
                    Frequency = scheduleDto.Frequency,
                    Time = scheduleDto.Time,
                    FinishDate = scheduleDto.FinishDate,
                    StartDate = DateTime.Now.ToString(),
                    Dosage = scheduleDto.Dosage,
                };

                _context.PillSchedules.Add(schedule);
                await _context.SaveChangesAsync();

            }


            return Ok();
        }

        [HttpGet("ForUser/{userId}")]
        public async Task<ActionResult<IEnumerable<PillSchedule>>> GetSchedule(int userId)
        {
            var schedules = await _context.PillSchedules
                .Include(s => s.Pill)
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