using Microsoft.AspNetCore.Builder;

namespace RequestCulture;

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCulture( this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCultureMiddleware>();
    }
}