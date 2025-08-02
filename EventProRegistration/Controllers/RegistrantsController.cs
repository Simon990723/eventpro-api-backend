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
    public class RegistrantsController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet("me")]
        public async Task<ActionResult<IEnumerable<MyRegistrationResponseDto>>> GetMyRegistrations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var registrations = await context.Registrants
                .Where(r => r.UserId == userId)
                .Include(r => r.Event)
                .Include(r => r.Invoice)
                .OrderByDescending(r => r.Event.Date)
                .Select(r => new MyRegistrationResponseDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Event = new MyRegistrationEventDto
                        { Name = r.Event.Name, Date = r.Event.Date, Location = r.Event.Location },
                    Invoice = new InvoiceDto { Id = r.Invoice.Id }
                })
                .ToListAsync();

            return Ok(registrations);
        }

        [HttpPost]
        public async Task<ActionResult<RegistrantResponseDto>> PostRegistrant(CreateRegistrantDto registrantDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var registeredEvent = await context.Events.FindAsync(registrantDto.EventId);
            if (registeredEvent == null)
                return BadRequest(new { message = "Event not found." });

            var registrant = new Registrant
            {
                Name = registrantDto.Name,
                Email = registrantDto.Email,
                EventId = registrantDto.EventId,
                UserId = userId
            };

            context.Registrants.Add(registrant);

            Invoice? newInvoice = null;
            if (registeredEvent.Price > 0)
            {
                newInvoice = new Invoice
                {
                    InvoiceNumber =
                        $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                    Amount = registeredEvent.Price,
                    IssuedDate = DateTime.UtcNow,
                    Status = "Paid",
                    Registrant = registrant
                };
                context.Invoices.Add(newInvoice);
            }

            await context.SaveChangesAsync();

            var responseDto = new RegistrantResponseDto
            {
                Id = registrant.Id,
                Name = registrant.Name,
                Email = registrant.Email,
                EventId = registrant.EventId,
                Invoice = (newInvoice != null ? new InvoiceDto { Id = newInvoice.Id } : null) ??
                          throw new InvalidOperationException()
            };

            return CreatedAtAction(nameof(GetRegistrant), new { id = registrant.Id }, responseDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registrant>>> GetRegistrants([FromQuery] int eventId)
        {
            if (eventId <= 0) return BadRequest("A valid eventId is required.");
            return await context.Registrants.Where(r => r.EventId == eventId).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Registrant>> GetRegistrant(int id)
        {
            var registrant = await context.Registrants.FindAsync(id);
            if (registrant == null) return NotFound();
            return registrant;
        }
    }
}