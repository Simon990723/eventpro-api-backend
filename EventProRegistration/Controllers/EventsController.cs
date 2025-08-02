using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventProRegistration.Data;
using EventProRegistration.Models;
using EventProRegistration.DTOs;

namespace EventProRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();
            return await context.Events.Where(e => e.UserId == userId).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var @event = await context.Events.FindAsync(id);
            if (@event == null) return NotFound();
            if (@event.UserId != userId) return Forbid();
            return @event;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEvent(int id, EventDto eventDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var eventToUpdate = await context.Events
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (eventToUpdate == null)
            {
                return NotFound();
            }

            eventToUpdate.Name = eventDto.Name;
            eventToUpdate.Location = eventDto.Location;
            eventToUpdate.Date = DateTime.SpecifyKind(eventDto.Date, DateTimeKind.Utc);
            eventToUpdate.Price = eventDto.Price;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(EventDto eventDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var newEvent = new Event
            {
                Name = eventDto.Name,
                Location = eventDto.Location,
                Date = DateTime.SpecifyKind(eventDto.Date, DateTimeKind.Utc),
                Price = eventDto.Price,
                UserId = userId
            };

            context.Events.Add(newEvent);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var @event = await context.Events.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (@event == null) return NotFound();

            context.Events.Remove(@event);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}