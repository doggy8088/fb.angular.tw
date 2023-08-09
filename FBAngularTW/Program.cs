using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureOptions<ShortUrlConfigureOptions>();

var app = builder.Build();

app.Run(ctx =>
{
    ctx.Response.Redirect(
        ctx.RequestServices
            .GetRequiredService<IOptionsMonitor<ShortUrlOptions>>()
            .Get(ctx.Request.Host.Host)
        .Url);
    return Task.CompletedTask;
});

app.Run();
