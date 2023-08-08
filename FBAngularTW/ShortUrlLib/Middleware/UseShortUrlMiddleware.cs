namespace FBAngularTW.ShortUrlLib.Middleware;

public class UseShortUrlMiddleware : IMiddleware
{
    private readonly ShortUrlService _shortUrlService;

    public UseShortUrlMiddleware(ShortUrlService shortUrlService)
    {
        _shortUrlService = shortUrlService;
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var url = _shortUrlService.GetUrl(context.Request.Host.Host);
        context.Response.Redirect(url);
        return Task.CompletedTask;
    }
}