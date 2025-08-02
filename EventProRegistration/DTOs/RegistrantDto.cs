using System.ComponentModel.DataAnnotations;

namespace EventProRegistration.DTOs
{
    public class CreateRegistrantDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int EventId { get; set; }
    }
    public class RegistrantResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int EventId { get; set; }
        public InvoiceDto Invoice { get; set; } 
    }

    public class InvoiceDto
    {
        public int Id { get; set; }
    }
    
    public class MyRegistrationEventDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        
        public string Location { get; set; }
    }
    
    public class MyRegistrationResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MyRegistrationEventDto Event { get; set; }
        public InvoiceDto Invoice { get; set; }
    }
}