using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using NSubstitute;

namespace FBAngularTW.Tests;

public class ShortUrlRedirectionMiddlewareTests
{
    private const string FALLBACK_URL = "https://www.fallback.com";

    private const string UNKNOWN_HOST          = "unknown.host.com";
    private const string KNOWN_HOST            = "known.host.com";
    private const string TARGET_FOR_KNOWN_HOST = "https://target.example.com";

    private ShortUrlRedirectionMiddleware _middleware = null!;

    private DefaultHttpContext _ctx = null!;

    private readonly IOptionsMonitor<RedirectionsOption> _mockOptionsMonitor = 
        Substitute.For<IOptionsMonitor<RedirectionsOption>>();

    [Fact]
    public async Task ShortUrlIsKnown()
    {
        // Arrange
        this.GivenHost(KNOWN_HOST);
        this.GivenOptions(new RedirectionsOption
        {
            FallbackUrl = FALLBACK_URL,
            Mappings = new[]
            {
                new RedirectionMapping
                {
                    SourceHost = KNOWN_HOST,
                    TargetUrl  = TARGET_FOR_KNOWN_HOST,
                },
            },
        });

        this.WhenConfiguredWith(this._mockOptionsMonitor);

        // Act
        await this._middleware.InvokeAsync(this._ctx);

        // Assert
        this.ShouldRedirectTo(TARGET_FOR_KNOWN_HOST);
    }

    [Fact]
    public async Task ShortUrlIsUnknown()
    {
        // Arrange
        this.GivenHost(UNKNOWN_HOST);
        this.GivenOptions(new RedirectionsOption
        {
            FallbackUrl = FALLBACK_URL,
            Mappings = new[]
            {
                new RedirectionMapping
                {
                    SourceHost = KNOWN_HOST,
                    TargetUrl  = TARGET_FOR_KNOWN_HOST,
                },
            },
        });

        this.WhenConfiguredWith(this._mockOptionsMonitor);

        // Act
        await this._middleware.InvokeAsync(this._ctx);

        // Assert
        this.ShouldRedirectTo(FALLBACK_URL);
    }

    private void WhenConfiguredWith(IOptionsMonitor<RedirectionsOption> optionsMonitor)
    {
        this._middleware = new ShortUrlRedirectionMiddleware(
            Substitute.For<RequestDelegate>(),
            optionsMonitor
        );
    }

    private void ShouldRedirectTo(string expectedTargetUrl)
    {
        Assert.Equal(expectedTargetUrl, this._ctx.Response.Headers["Location"]);
    }

    private void GivenOptions(RedirectionsOption redirectionsOption)
    {
        this._mockOptionsMonitor.CurrentValue.Returns(redirectionsOption);
    }

    private void GivenHost(string hostName)
    {
        this._ctx = new DefaultHttpContext
        {
            Request =
            {
                Host = new HostString(hostName),
            },
        };
    }
}