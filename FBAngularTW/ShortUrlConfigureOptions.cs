using Microsoft.Extensions.Options;



internal class ShortUrlConfigureOptions : IConfigureNamedOptions<RedirectUrlOptions>
{
    private readonly IConfiguration _configuration;
    public ShortUrlConfigureOptions(IConfiguration configuration) {
        _configuration = configuration.GetSection(RedirectUrlOptions.OPTIONS_NAME);
    }   
    public void Configure(string? name, RedirectUrlOptions options)
        => _configuration.Bind(name ?? RedirectUrlOptions.DEFAULT_NAME, options);

    public void Configure(RedirectUrlOptions options)
        => Configure(string.Empty, options);
}

