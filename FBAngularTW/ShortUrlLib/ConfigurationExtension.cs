using FBAngularTW.ShortUrlLib.Middleware;
using FBAngularTW.ShortUrlLib.Models;

namespace FBAngularTW.ShortUrlLib
{
    public static class ConfigurationExtension
    {
        public static WebApplicationBuilder AddShortUrl(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ShortUrlSetting>(builder.Configuration.GetSection(nameof(ShortUrlSetting)));
            builder.Services.AddScoped<ShortUrlService>();
            builder.Services.AddScoped<UseShortUrlMiddleware>();

            return builder;
        }
    }
}
