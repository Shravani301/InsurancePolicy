using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.Models
{
    public class InsuranceScheme
    {
        [Key]
        public Guid SchemeId { get; set; }

        [Required(ErrorMessage = "Scheme Name is required.")]
        [StringLength(100, ErrorMessage = "Scheme Name should not exceed 100 characters.")]
        public string SchemeName { get; set; }
        public bool Status { get; set; }

        public SchemeDetails? SchemeDetails { get; set; }
        public List<Policy>? Policies { get; set; }
        public List<InsurancePlan>? Plans { get; set; }

    }
}
