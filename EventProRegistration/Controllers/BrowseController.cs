using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventProRegistration.Data;
using EventProRegistration.Models;

namespace EventProRegistration.Controllers
{
    [Route("api/[controller]/events")]
    [ApiController]
    public class BrowseController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            return await context.Events.OrderByDescending(e => e.Date).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Event>> GetPublicEventById(int id)
        {
            var @event = await context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }
    }
}