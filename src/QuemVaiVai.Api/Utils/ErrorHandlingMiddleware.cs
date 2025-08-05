using Microsoft.AspNetCore.Http;
using QuemVaiVai.Api.Responses;
using QuemVaiVai.Domain.Exceptions;
using System.Text.Json;

namespace QuemVaiVai.Api.Utils;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var origin = context.Request.Headers.Origin.ToString();

            var corsOrigin = context.Response.Headers["Access-Control-Allow-Origin"].ToString();

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                InvalidPasswordException => StatusCodes.Status400BadRequest,
                EmailAlreadyExistsException => StatusCodes.Status400BadRequest,
                InvalidModelStateException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var error = new ErrorResponse(false, [ex.Message, ex.StackTrace]);

            var json = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(json);
        }
    }
}
