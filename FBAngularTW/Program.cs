using FBAngularTW;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var shortUrlProvider = new ShortUrlProvider(builder.Configuration);


app.Run(ctx =>
{
    var shortUrl = shortUrlProvider.GetShortUrl(ctx.Request.Host.Host);
    ctx.Response.Redirect(shortUrl);
    return Task.CompletedTask;
});

app.Run();
