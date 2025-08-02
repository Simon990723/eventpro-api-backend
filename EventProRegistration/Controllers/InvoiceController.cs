using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventProRegistration.Data;
using EventProRegistration.Services;

namespace EventProRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(ApplicationDbContext context) : ControllerBase
    {
        private readonly PdfService _pdfService = new();

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvoicePdf(int id)
        {
            var invoice = await context.Invoices
                .Include(i => i.Registrant)
                .ThenInclude(r => r.Event)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            var pdfBytes = PdfService.GenerateInvoicePdf(invoice);

            return File(pdfBytes, "application/pdf", $"Invoice-{invoice.InvoiceNumber}.pdf");
        }
    }
}