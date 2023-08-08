namespace FBAngularTW.ShortUrlLib.Middleware;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseShortUrl(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UseShortUrlMiddleware>();
    }
}