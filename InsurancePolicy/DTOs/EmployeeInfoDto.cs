﻿using System.ComponentModel.DataAnnotations;

namespace InsurancePolicy.DTOs
{
    public class EmployeeInfoDto
    {
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name should be between 3 and 50 characters.")]
        public string EmployeeFirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name should not exceed 50 characters.")]
        public string EmployeeLastName { get; set; }

        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phone number must start with 6-9 and contain exactly 10 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        public string Email { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
        public double Salary { get; set; }

        public bool Status { get; set; }
    }
}
