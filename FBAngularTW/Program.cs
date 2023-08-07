using FBAngularTW.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions<AppSettings>()
    .Bind(builder.Configuration)
    .ValidateDataAnnotations()
    .ValidateOnStart();
var app = builder.Build();

app.Run(ctx =>
{
    var appSettings = app.Services.GetRequiredService<IOptionsMonitor<AppSettings>>();

    var target = appSettings.CurrentValue.RoutePairs.SingleOrDefault(x => x.Url == ctx.Request.Host.Host) 
        ?? appSettings.CurrentValue.RoutePairs.SingleOrDefault(x => x.Url == "default");
    if (target != null)
    {
        ctx.Response.Redirect(target.RedirectUrl);
    }
    return Task.CompletedTask;
});

app.Run();
