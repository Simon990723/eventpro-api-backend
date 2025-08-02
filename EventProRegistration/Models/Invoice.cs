using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventProRegistration.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string InvoiceNumber { get; set; } 

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime IssuedDate { get; set; }

        [Required]
        public string Status { get; set; } 

        public int RegistrantId { get; set; }
        public virtual Registrant Registrant { get; set; }
    }
}