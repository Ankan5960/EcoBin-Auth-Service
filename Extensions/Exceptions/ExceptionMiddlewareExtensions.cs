using EcoBin_Auth_Service.Model.DTOs;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace EcoBin_Auth_Service.Extensions.Exceptions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var exception = contextFeature.Error;

                    context.Response.StatusCode = exception switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                        InvalidOperationException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    logger.LogError(exception, "An exception occurred: {Message}", exception.Message);

                    var innerExceptions = new List<string>();
                    var currentException = exception.InnerException;
                    while (currentException != null)
                    {
                        innerExceptions.Add(currentException.Message);
                        currentException = currentException.InnerException;
                    }

                    var errorResponse = new ErrorDetailsDto
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorType = exception.GetType().Name,
                        Message = exception.Message,
                        DetailedMessage = exception.ToString(),
                        InnerExceptions = innerExceptions.Any() ? innerExceptions : null
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
                }
            });
        });
    }
}
