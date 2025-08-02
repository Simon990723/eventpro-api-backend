using EventProRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventProRegistration.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<IdentityUser, IdentityRole, string>(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Registrant> Registrants { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}