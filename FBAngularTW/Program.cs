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

    var targetDomainUrl = domainUrlConfig.Entries.SingleOrDefault(x => x.Domain == ctx.Request.Host.Host);
    if (targetDomainUrl == null)
    {
        ctx.Response.Redirect("https://www.facebook.com/will.fans");
    }
    else
    {
        ctx.Response.Redirect(targetDomainUrl.Url);
    }
   
    return Task.CompletedTask;
});

app.Run();
