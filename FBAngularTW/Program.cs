using FBAngularTW;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.Configure<DomainUrlConfig>(options => configuration.GetSection("DomainUrlConfigs").Bind(options));
var app = builder.Build();

app.Run(ctx =>
{
    IOptionsMonitor<DomainUrlConfig> domainUrlConfigOption = ctx.RequestServices.GetRequiredService<IOptionsMonitor<DomainUrlConfig>>();

    var domainUrlConfig = domainUrlConfigOption.CurrentValue;
    if (domainUrlConfig.Mapping.TryGetValue(ctx.Request.Host.Host, out string targetDomainUrl))
    {
        ctx.Response.Redirect(targetDomainUrl);
    }
    else
    {
        ctx.Response.Redirect("https://www.facebook.com/will.fans");
    }
   
    return Task.CompletedTask;
});

app.Run();
