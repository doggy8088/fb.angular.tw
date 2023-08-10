using FBAngularTW;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RedirectSetting>(builder.Configuration.GetSection("RedirectSetting"));
builder.Services.AddSingleton<RedirectService>();
var app = builder.Build();

app.Run(ctx =>
{
    RedirectService service = ctx.RequestServices.GetService<RedirectService>();
    if (service != null)
        ctx.Response.Redirect(service.GetTargetUrl(ctx.Request.Host.Host));
    return Task.CompletedTask;
});

app.Run();