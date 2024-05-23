var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

RewriteOptions rewriteOptions = new RewriteOptions();

rewriteOptions.Add(new RedirectAngularHostRule(
    "fb.angular.tw",
    "https://www.facebook.com/groups/augularjs.tw"
));
rewriteOptions.Add(new RedirectAngularHostRule(
    "yt.angular.tw",
    "https://www.youtube.com/c/AngularUserGroupTaiwan/videos"
));
rewriteOptions.Add(new RedirectAngularHostRule(
    "ts.angular.tw",
    "https://willh.gitbook.io/typescript-tutorial"
));
rewriteOptions.Add(new RedirectAngularHostRule(
    "vscode.angular.tw",
    "https://marketplace.visualstudio.com/items?itemName=doggy8088.angular-extension-pack"
));
rewriteOptions.Add(new RedirectAngularHostRule(
    "cli.angular.tw",
    "https://youtu.be/v4_YsDZbs3g"
));
rewriteOptions.Add(new RedirectAngularHostRule(
    "rx6.angular.tw",
    "https://youtu.be/BA1vSZwzkK8"
));
rewriteOptions.Add(new RedirectAngularHostRule(
    "install.angular.tw",
    "https://gist.github.com/doggy8088/15e434b43992cf25a78700438743774a"
));

rewriteOptions.Add(new RedirectAngularHostRule(
    "devtools.angular.tw",
    "https://chromewebstore.google.com/detail/angular-devtools/ienfalfjdbdpebioblfackkekamfmbnh"
));

rewriteOptions.Add(new RedirectAngularHostRule());

app.UseRewriter(rewriteOptions);

app.Run(ctx =>
{
    return Task.CompletedTask;
});

app.Run();

public sealed class RedirectAngularHostRule : IRule
{
	private readonly string? _domain;
	private readonly string _url;
	private readonly int _statusCode = 301;

	public RedirectAngularHostRule()
	{
		_domain = default;
		_url = "https://www.facebook.com/will.fans";
	}

	public RedirectAngularHostRule(string? domain, string url, int statusCode = 301)
    {
        _domain = domain;
		_url = url;
        _statusCode = statusCode;
    }

    public void ApplyRule(RewriteContext context)
    {
        var req = context.HttpContext.Request;

		if (_domain is null || (req.Host.HasValue && req.Host.Value.Equals(_domain, StringComparison.OrdinalIgnoreCase)))
		{
			var request = context.HttpContext.Request;
			var response = context.HttpContext.Response;

			response.StatusCode = _statusCode;
			response.Headers.Location = _url;
			context.Result = RuleResult.EndResponse;
			return;
        }

		context.Result = RuleResult.ContinueRules;
	}
}
