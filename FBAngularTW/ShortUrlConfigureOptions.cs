using Microsoft.Extensions.Options;

internal sealed class ShortUrlConfigureOptions : IPostConfigureOptions<ShortUrlOptions>, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IOptionsMonitorCache<ShortUrlOptions> _cache;
    private readonly List<string> _cacheNames = new List<string>();
    private IDisposable _disposer;
    public ShortUrlConfigureOptions(IConfiguration configuration, IOptionsMonitorCache<ShortUrlOptions> cache)
    {
        _configuration = configuration;
        _cache = cache;
        _disposer = _configuration.GetReloadToken()
            .RegisterChangeCallback(Callback, null);

        // remove cache when reload
        void Callback(object? state)
        {
            _cacheNames.ForEach(name => _cache.TryRemove(name));
            _cacheNames.Clear();
            _disposer = _configuration.GetReloadToken()
                .RegisterChangeCallback(Callback, null);
        }
    }

    public string? Name { get; } = nameof(ShortUrlOptions);

    public void Configure(ShortUrlOptions options)
        => PostConfigure(string.Empty, options);

    public void Dispose()
    {
        _disposer.Dispose();
    }

    public void PostConfigure(string? name, ShortUrlOptions options)
    {
        var section = _configuration.GetSection(ShortUrlOptions.OPTIONS_NAME);
        options.Url = name switch
        {
            string url when url.Length > 0 => section.GetValue<string>(url),
            _ => section.GetValue<string>(ShortUrlOptions.DEFAULT_NAME)
        } ?? string.Empty;

        // mark cached name
        _cacheNames.Add(name!);
    }
}

