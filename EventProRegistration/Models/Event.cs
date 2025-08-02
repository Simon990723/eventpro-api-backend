using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using Microsoft.AspNetCore.Identity;

namespace EventProRegistration.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Price { get; set; }

        public virtual ICollection<Registrant> Registrants { get; set; } = new List<Registrant>();

        [Required]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}