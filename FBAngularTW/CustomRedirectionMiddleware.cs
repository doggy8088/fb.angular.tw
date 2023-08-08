using Microsoft.Extensions.Options;

namespace FBAngularTW;

public class CustomRedirectionMiddleware
{
    private const string FB_WILL_FANS = "https://www.facebook.com/will.fans";

    private readonly RequestDelegate            _next;
    private readonly Dictionary<string, string> _lookup;

    public CustomRedirectionMiddleware(
        RequestDelegate next,
        IOptionsMonitor<List<RedirectionOption>> optionMonitor
    )
    {
        this._next = next;

        this._lookup = new Dictionary<string, string>();
        this.UpdateLookup(optionMonitor.CurrentValue);

        optionMonitor.OnChange(this.UpdateLookup);
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        await this._next(ctx);
        ctx.Response.Redirect(
            this._lookup.TryGetValue(ctx.Request.Host.Host, out var targetUrl)
                ? targetUrl
                : FB_WILL_FANS
        );

    }

    private void UpdateLookup(List<RedirectionOption> currentOptions)
    {
        this._lookup.Clear();
        foreach (var option in currentOptions)
        {
            this._lookup[option.SourceHost] = option.TargetUrl;
        }
    }
}