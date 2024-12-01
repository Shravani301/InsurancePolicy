using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.DTOs
{
    public class CustomerRequestDto
    {
        public Guid? CustomerId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name should not exceed 50 characters.")]
        public string CustomerFirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name should not exceed 50 characters.")]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phone number must start with 6-9 and contain exactly 10 digits.")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "Address should not exceed 100 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string? State { get; set; }

        [StringLength(50, ErrorMessage = "City should not exceed 50 characters.")]
        public string? City { get; set; }

        public bool Status { get; set; }

        public Guid? AgentId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters long.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters long.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; } = string.Empty;
    }

}
