using FBAngularTW;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MySitesOptions>(builder.Configuration.GetSection(MySitesOptions.Position));
var app = builder.Build();

app.Run(ctx =>
{
    var mysites = app.Services.GetRequiredService<IOptions<MySitesOptions>>().Value;
    if (mysites.Site.ContainsKey(ctx.Request.Host.Host))
    {
        ctx.Response.Redirect(mysites.Site[ctx.Request.Host.Host]);
    }
    else
    {
        ctx.Response.Redirect(mysites.Default);
    }
    return Task.CompletedTask;
});

app.Run();
