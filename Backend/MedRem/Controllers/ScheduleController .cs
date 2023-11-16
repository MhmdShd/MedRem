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
            string frequencyCommaSeparated = String.Join(", ", scheduleDto.Frequency);
            if (scheduleDto.PillId.HasValue && _context.Pills.Any(p => p.Id == scheduleDto.PillId.Value))
            {

                foreach (var time in scheduleDto.Time)
                {

                    var schedule = new PillSchedule
                    {
                        UserId = scheduleDto.UserId,
                        PillId = scheduleDto.PillId.Value,
                        Frequency = frequencyCommaSeparated,
                        Dosage = scheduleDto.Dosage,
                        FinishDate = scheduleDto.FinishDate,
                        StartDate = DateTime.Now.ToString(),
                        Time = TimeOnly.Parse(time),
                    };
                    _context.PillSchedules.Add(schedule);
                    await _context.SaveChangesAsync();
                }


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

                foreach (var time in scheduleDto.Time)
                {

                    //var schedule = new PillSchedule
                    //{
                    //    UserId = scheduleDto.UserId,
                    //    PillId = scheduleDto.PillId.Value,
                    //    Frequency = frequencyCommaSeparated,
                    //    Dosage = scheduleDto.Dosage,
                    //    FinishDate = scheduleDto.FinishDate,
                    //    StartDate = DateTime.Now.ToString(),
                    //    Time = TimeOnly.Parse(time),
                    //};
                    var schedule = new PillSchedule();
                    schedule.UserId=scheduleDto.UserId;
                    schedule.PillId = pill.Id ;
                    schedule.Frequency = frequencyCommaSeparated; 
                    schedule.Dosage = scheduleDto.Dosage; ;
                    schedule.FinishDate = scheduleDto.FinishDate; 
                    schedule.StartDate = DateTime.Now.ToString(); 
                    schedule.Time = TimeOnly.Parse(time);

                    _context.PillSchedules.Add(schedule);
                    await _context.SaveChangesAsync();
                }
            }


            return Ok();
        }

        [HttpGet("ForUser/{userId}")]
        //public async Task<ActionResult<IEnumerable<PillScheduleDto>>> GetSchedule(int userId)
        //{
        //    var schedules = await _context.PillSchedules.Include(x => x.Pill)
        //                                   .Where(s => s.UserId == userId)
        //                                   .Select(s => new PillScheduleDto
        //                                   {
        //                                        Map properties to DTO
        //                                   })
        //                                   .ToListAsync();
        //    if (schedules == null)
        //    {
        //        return NotFound();
        //    }
        //    return schedules;
        //}
        public async Task<ActionResult<IEnumerable<PillSchedule>>> GetSchedule(int userId)
        {
            var schedules = await _context.PillSchedules.Include(x=>x.Pill)
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