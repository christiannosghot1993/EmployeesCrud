using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeesManagement.Api.Filters;

public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var (status, title) = exception switch
        {
            ArgumentException => (400, "Validation failed."),
            UnauthorizedAccessException => (401, "Authentication failed."),
            KeyNotFoundException => (404, "Resource not found."),
            InvalidOperationException => (409, "Conflict."),
            _ => (500, "An unexpected error occurred.")
        };

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = exception.Message
        };

        if (status == 400)
            problem.Extensions["errors"] = new Dictionary<string, string[]> { ["request"] = [exception.Message] };

        context.Result = new ObjectResult(problem) { StatusCode = status };
        context.ExceptionHandled = true;
    }
}