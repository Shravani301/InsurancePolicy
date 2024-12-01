namespace InsurancePolicy.DTOs
{
    public class CustomerResponseDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public bool Status { get; set; }
        public string? UserName { get; set; }
        public int TotalPolicies { get; set; }
        public string? AgentName { get; set; }
    }

}
