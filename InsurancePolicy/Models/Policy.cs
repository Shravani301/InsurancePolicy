using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsurancePolicy.Models
{
    public class Policy
    {
        [Key]
        public Guid PolicyId { get; set; }

        [Required(ErrorMessage = "Issue Date is required.")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Maturity Date is required.")]
        public DateTime MaturityDate { get; set; }

        [Required(ErrorMessage = "Premium Type is required.")]
        public Term PremiumType { get; set; }

        [Required(ErrorMessage = "Premium Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Premium Amount must be greater than 0.")]
        public double PremiumAmount { get; set; }

        [Required(ErrorMessage = "Sum Assured is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Sum Assured must be greater than 0.")]
        public double SumAssured { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage = "Insurance Scheme is required.")]
        [ForeignKey("InsuranceScheme")]
        public Guid InsuranceSchemeId { get; set; }

        public List<Customer>? Customers { get; set; }
        public List<Payment>? Payment { get; set; }
        public List<Claim> Claim { get; set; }
        public InsuranceScheme? InsuranceScheme { get; set; } 

    }
}
