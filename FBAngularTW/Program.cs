using FBAngularTW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.Configure<DomainUrlConfig>(options => configuration.GetSection("DomainUrlConfigs").Bind(options));
var app = builder.Build();

app.Run(ctx =>
{
    var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using var serviceScope = serviceScopeFactory.CreateScope();
    IOptionsSnapshot<DomainUrlConfig> domainUrlConfigOption = serviceScope.ServiceProvider.GetRequiredService<IOptionsSnapshot<DomainUrlConfig>>();

    var domainUrlConfig = domainUrlConfigOption.Value;

    var targetDomainUr = domainUrlConfig.Entries.SingleOrDefault(x => x.Domain == ctx.Request.Host.Host);
    if (targetDomainUr == null)
    {
        ctx.Response.Redirect("https://www.facebook.com/will.fans");
    }
    else
    {
        ctx.Response.Redirect($"{targetDomainUr.Url}");
    }
   
    return Task.CompletedTask;
});

app.Run();
