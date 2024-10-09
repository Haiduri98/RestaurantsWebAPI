using System.Net;
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);
            logger.LogError(notFound.Message);
        }
        catch (ForbidException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access Forbidden");
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong.");
        }
    }
}