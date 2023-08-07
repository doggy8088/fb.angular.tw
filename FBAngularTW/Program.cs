using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<ShortUrlConfigureOptions>();
var app = builder.Build();

app.Run(ctx =>
{
    ctx.Response.Redirect(
        ctx.RequestServices
            .GetRequiredService<IOptionsSnapshot<ShortUrlOptions>>()
            .Get(ctx.Request.Host.Host)
        .RedirectUrl);

    // switch (ctx.Request.Host.Host)
    // {
    //     case "fb.angular.tw":
    //         ctx.Response.Redirect("https://www.facebook.com/groups/augularjs.tw");
    //         break;

    //     case "yt.angular.tw":
    //         ctx.Response.Redirect("https://www.youtube.com/c/AngularUserGroupTaiwan/videos");
    //         break;
            
    //     case "ts.angular.tw":
    //         ctx.Response.Redirect("https://willh.gitbook.io/typescript-tutorial");
    //         break;

    //     case "vscode.angular.tw":
    //         ctx.Response.Redirect("https://marketplace.visualstudio.com/items?itemName=doggy8088.angular-extension-pack");
    //         break;

    //     case "cli.angular.tw":
    //         ctx.Response.Redirect("https://youtu.be/v4_YsDZbs3g");
    //         break;

    //     case "rx6.angular.tw":
    //         ctx.Response.Redirect("https://youtu.be/BA1vSZwzkK8");
    //         break;

    //     case "install.angular.tw":
    //         ctx.Response.Redirect("https://gist.github.com/doggy8088/15e434b43992cf25a78700438743774a");
    //         break;

    //     default:
    //         ctx.Response.Redirect("https://www.facebook.com/will.fans");
    //         break;
    // }
    return Task.CompletedTask;
});

app.Run();
