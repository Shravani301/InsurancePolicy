using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; } 

        [Required(ErrorMessage = "Payment type is required.")]
        public PaymentType paymentType { get; set; }

        [Required(ErrorMessage = "Amount paid is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Amount paid must be a positive number.")]
        public double AmountPaid { get; set; } 

        [Required(ErrorMessage = "Tax is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Tax must be a positive number.")]
        public double Tax { get; set; } 

        [Required(ErrorMessage = "Total payment is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Total payment must be a positive number.")]
        public double TotalPayment { get; set; } 

        [Required(ErrorMessage = "Payment date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid payment date.")]
        public DateTime PaymentDate { get; set; } // Payment date

        public List<Policy> Policies { get; set; }
        public Payment()
        {
            PaymentDate = DateTime.Now; 
        }
    }
}
