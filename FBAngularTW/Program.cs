var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(ctx =>
{
    var host = ctx.Request.Host.Host;
    var redirectUrl = builder.Configuration.GetValue<String>($"RedirectUrls:{host}");
    var defaultUrl = builder.Configuration.GetValue<String>($"RedirectUrls:default");
    redirectUrl= (redirectUrl != null)? redirectUrl:string.Format($"{defaultUrl}");    
    ctx.Response.Redirect(redirectUrl);
    return Task.CompletedTask;
});

app.Run();
