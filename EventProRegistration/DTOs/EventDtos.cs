using System.ComponentModel.DataAnnotations;

namespace EventProRegistration.DTOs
{
    public class EventDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }
        
        public decimal Price { get; set; }
    }
}