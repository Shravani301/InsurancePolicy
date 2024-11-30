using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.Models
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; }
                
        [Required(ErrorMessage = "Document Name is required.")]
        [StringLength(250, ErrorMessage = "Document Name should not exceed 100 characters.")]
        public DocumentType DocumentName { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        public string DocumentPath { get; set; }

        public Status Status { get; set; }

        [Required(ErrorMessage = "Customer is required.")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
