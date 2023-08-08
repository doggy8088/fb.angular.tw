namespace FBAngularTW
{
    public class ShortUrlMiddleware
    {
        private ShortUrlService _shortUrlService;

        public ShortUrlMiddleware(RequestDelegate next, ShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var shortUrl = _shortUrlService.GetShortUrl(httpContext.Request.Host.Host);
            httpContext.Response.Redirect(shortUrl);
            return Task.CompletedTask;
        }
    }
}
