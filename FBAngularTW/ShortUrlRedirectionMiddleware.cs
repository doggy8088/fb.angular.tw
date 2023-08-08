using Microsoft.Extensions.Options;

namespace FBAngularTW;

public class ShortUrlRedirectionMiddleware
{
    private readonly RequestDelegate            _next;
    
    private string _fallbackUrl = null!;
    private readonly Dictionary<string, string> _lookup;

    public ShortUrlRedirectionMiddleware(
        RequestDelegate next,
        IOptionsMonitor<RedirectionsOption> optionsMonitor
    )
    {
        this._next = next;

        this._lookup = new Dictionary<string, string>();
        this.UpdateRedirections(optionsMonitor.CurrentValue);

        optionsMonitor.OnChange(this.UpdateRedirections);
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        await this._next(ctx);
        ctx.Response.Redirect(
            this._lookup.TryGetValue(ctx.Request.Host.Host, out var targetUrl)
                ? targetUrl
                : this._fallbackUrl
        );

    }

    private void UpdateRedirections(RedirectionsOption option)
    {
        this._fallbackUrl = option.FallbackUrl;
        
        this._lookup.Clear();
        foreach (var mapping in option.Mappings)
        {
            this._lookup[mapping.SourceHost] = mapping.TargetUrl;
        }
    }
}