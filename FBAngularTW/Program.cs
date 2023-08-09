using FBAngularTW;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RedirectSetting>(builder.Configuration.GetSection("RedirectSetting"));
builder.Services.AddSingleton<RedirectService>();
var app = builder.Build();

app.Run(ctx =>
{
    ctx.Response.Redirect(
    ctx.RequestServices.GetService<RedirectService>().GetTargetUrl(ctx)
    );
    return Task.CompletedTask;
});

app.Run();