using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace EventProRegistration.Models
{
    public class Registrant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public virtual Invoice Invoice { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}