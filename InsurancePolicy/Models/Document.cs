using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.Models
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; }

        [Required(ErrorMessage = "Document Type is required.")]
        [StringLength(50, ErrorMessage = "Document Type should not exceed 50 characters.")]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        [StringLength(250, ErrorMessage = "Document Name should not exceed 100 characters.")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Customer is required.")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
