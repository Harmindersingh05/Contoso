using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);               
        }
        catch (ValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred"
            };

            if (exception.Errors is not null)
            {
                var errors = exception.Errors.Select(m => new { m.PropertyName, m.ErrorMessage, m.ErrorCode }).ToList();
                problemDetails.Extensions["errors"] = errors;
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}