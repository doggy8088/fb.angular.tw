using FBAngularTW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RedirectInfos>(builder.Configuration.GetSection(nameof(RedirectInfos)));
var app = builder.Build();

app.Run(ctx =>
{
    var redirectInofs = ctx.RequestServices.GetRequiredService<IOptionsSnapshot<RedirectInfos>>()?.Value;

    var redirectUrl = redirectInofs.RedirectList?
    .FirstOrDefault(x => x.Host == ctx.Request.Host.Host)?.Url ?? redirectInofs.DefaultUrl;
    
    if (string.IsNullOrEmpty(redirectUrl))
    {
        ctx.Response.StatusCode = (int)HttpStatusCode.NotFound;
    }
    ctx.Response.Redirect(redirectUrl);

    return Task.CompletedTask;
});

app.Run();
