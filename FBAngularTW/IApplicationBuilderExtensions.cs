namespace FBAngularTW;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseShortUrlRedirection(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ShortUrlRedirectionMiddleware>();
    } 
}