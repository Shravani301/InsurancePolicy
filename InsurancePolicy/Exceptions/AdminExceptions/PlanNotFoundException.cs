namespace InsurancePolicy.Exceptions.AdminExceptions
{
    public class PlanNotFoundException:Exception
    {
        public PlanNotFoundException(string message) : base(message) { }
    }
}
