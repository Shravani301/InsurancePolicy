using InsurancePolicy.Exceptions.AdminExceptions;
using InsurancePolicy.Exceptions.AgentExceptions;
using InsurancePolicy.Exceptions.ClaimExceptions;
using InsurancePolicy.Exceptions.CustomerExceptions;
using InsurancePolicy.Exceptions.DocumentExceptions;
using InsurancePolicy.Exceptions.EmployeeExceptions;
using InsurancePolicy.Exceptions.PaymentExceptions;
using InsurancePolicy.Exceptions.PlanExceptions;
using InsurancePolicy.Exceptions.PolicyExceptions;
using InsurancePolicy.Exceptions.RoleException;
using InsurancePolicy.Exceptions.SchemeDetailsExceptions;
using InsurancePolicy.Exceptions.SchemeExceptions;
using InsurancePolicy.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace InsurancePolicy.Exceptions
{
    public class AppExceptionHandler:IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
           Exception exception, CancellationToken cancellationToken)
        {

            var response = new ErrorResponse();
            if (exception is AdminExceptions.PlanNotFoundException || exception is AgentNotFoundException ||exception is ClaimNotFoundException|| exception is CustomerExceptions.SchemeNotFoundException ||
    exception is DocumentNotFoundException || exception is EmployeeNotFoundException || exception is SchemeExceptions.SchemeNotFoundException ||
    exception is RoleNotFoundException || exception is PlanExceptions.PlanNotFoundException||exception is PolicyNotFoundException||exception is SchemeDetailsNotFoundException
    ||exception is PaymentNotFoundException)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else if (exception is AdminsDoesNotExistException || exception is AgentsDoesNotExistException || exception is ClaimsDoesNotExistException||exception is CustomersDoesNotExistException ||
         exception is DocumentsDoesNotExistException || exception is EmployeesDoesNotExistException ||
         exception is RolesDoesNotExistException || exception is PlansDoesNotExistException || exception is SchemesDoesNotExistException
         ||exception is PoliciesDoesNotExistException || exception is SchemeDetailsDoesNotExistException||exception is PaymentsDoesNotExistException)
            {
                response.StatusCode = StatusCodes.Status204NoContent;
                response.ExceptionMessage = exception.Message;
                response.Title = "empty []";
            }
            else
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ExceptionMessage = exception.Message;
                response.Title = "Something went wrong!";
            }
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
