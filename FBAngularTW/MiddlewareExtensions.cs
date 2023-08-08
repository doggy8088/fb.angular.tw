namespace FBAngularTW
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseShortUrl(
          this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShortUrlMiddleware>();
        }
    }
}
