using FBAngularTW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions<RedirectInfos>()
    .Bind(builder.Configuration.GetSection(nameof(RedirectInfos)))
    .ValidateDataAnnotations()
    .ValidateOnStart();
var app = builder.Build();

app.Run(ctx =>
{
    var host = ctx.Request.Host.Host;

    var redirectInofs = ctx.RequestServices
    .GetRequiredService<IOptionsSnapshot<RedirectInfos>>().Value;

    var redirectUrl = redirectInofs.RedirectList?
    .FirstOrDefault(x => x.Host == host)?.Url;

    if (string.IsNullOrEmpty(redirectUrl))
    {
        redirectUrl = redirectInofs.DefaultUrl;
    }

    ctx.Response.Redirect(redirectUrl);

    return Task.CompletedTask;
});

app.Run();
