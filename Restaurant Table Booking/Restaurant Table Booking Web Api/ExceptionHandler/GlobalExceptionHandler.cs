using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Shared.Constants.UserConstants;

namespace Restaurant_Table_Booking_Web_Api.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name,
                Title = UnhandledError,
                Detail = exception.Message
            };
            await httpContext
                .Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
