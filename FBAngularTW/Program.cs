using FBAngularTW;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(ctx =>
{
    var host = ctx.Request.Host.Host;
    var redirectUrlOptions = new List<RedirectUrlOption>();
    builder.Configuration.GetSection(RedirectUrlOption.RedirectUrls).Bind(redirectUrlOptions);
    var option = redirectUrlOptions.FirstOrDefault(x => x.host == host);
    if (option is null) option = redirectUrlOptions.FirstOrDefault(x => x.host == RedirectUrlOption.DefaultHost);    
    if (option is not null) ctx.Response.Redirect(option.url);
    return Task.CompletedTask;
});

app.Run();
