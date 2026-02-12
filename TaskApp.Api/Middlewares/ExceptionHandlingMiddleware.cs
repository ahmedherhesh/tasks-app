using System.Net;
using System.Text.Json;
using FluentValidation;
using TaskApp.Application.Shared.Responses;

namespace TaskApp.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        // validation
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors.GroupBy(e => e.PropertyName)
                           .ToDictionary(g => g.Key, g => string.Join(", ", g.Select(e => e.ErrorMessage)));
            var response = new Response<Dictionary<string, string>>(data: errors, success: false, message: ex.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        // any other exception
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = JsonSerializer.Serialize(new Response<object>(success: false, message: ex.Message));
            await context.Response.WriteAsync(response);
        }
    }
}
