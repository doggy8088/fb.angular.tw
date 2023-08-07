using Microsoft.Extensions.Options;



internal class ShortUrlConfigureOptions : IConfigureNamedOptions<ShortUrlOptions>
{
    private readonly IConfiguration _configuration;
    public ShortUrlConfigureOptions(IConfiguration configuration) {
        _configuration = configuration.GetSection(ShortUrlOptions.OPTIONS_NAME);
    }   
    public void Configure(string? name, ShortUrlOptions options)
        => options.RedirectUrl = name switch {
            string url when url.Length > 0 => _configuration.GetValue<string>(url),
            _ => _configuration.GetValue<string>(ShortUrlOptions.DEFAULT_NAME)
        } ?? string.Empty;

    public void Configure(ShortUrlOptions options)
        => Configure(string.Empty, options);
}

