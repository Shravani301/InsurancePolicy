using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.Models
{
    public class TaxSettings
    {
        [Key]
        public Guid TaxId { get; set; } // Changed to Guid for consistency

        [Required(ErrorMessage = "Tax Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Tax Percentage must be between 0 and 100.")]
        public double TaxPercentage { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation property for related insurance policies
        //public List<Policy> InsurancePolicies { get; set; }
    }
}
