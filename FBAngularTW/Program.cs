using FBAngularTW;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(ctx =>
{
    var host = ctx.Request.Host.Host;
    host = "angular.tw";
    var redirectUrlOptions = new List<RedirectUrlOption>();
    builder.Configuration.GetSection(RedirectUrlOption.RedirectUrl).Bind(redirectUrlOptions);
    var option = redirectUrlOptions.FirstOrDefault(x => x.host == host);            
    if (option is not null)
    {
        ctx.Response.Redirect(option.url);
    }
    return Task.CompletedTask;
});

app.Run();
