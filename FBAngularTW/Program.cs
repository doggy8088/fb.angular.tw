var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(ctx =>
{
    switch (ctx.Request.Host.Host)
    {
        case "fb.angular.tw":
            ctx.Response.Redirect("https://www.facebook.com/groups/augularjs.tw");
            break;

        case "yt.angular.tw":
            ctx.Response.Redirect("https://www.youtube.com/c/AngularUserGroupTaiwan/videos");
            break;
            
        case "ts.angular.tw":
            ctx.Response.Redirect("https://willh.gitbook.io/typescript-tutorial ");
            break;

        default:
            ctx.Response.Redirect("https://www.facebook.com/groups/augularjs.tw");
            break;
    }
    return Task.CompletedTask;
});

app.Run();
