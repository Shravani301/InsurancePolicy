using InsurancePolicy.Exceptions.EmployeeExceptions;
using InsurancePolicy.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace InsurancePolicy.Exceptions.CustomerExceptions
{
    public class CustomerExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
          Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponse();
            if (exception is SchemeNotFoundException)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.ExceptionMessage = exception.Message;
                response.Title = "Wrong Input";
            }
            else if (exception is CustomersDoesNotExistException)
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
