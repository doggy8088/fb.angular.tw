using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class ServiceProviderClassFixture<T> where T : class {
    private static readonly IServiceCollection _services 
        = new ServiceCollection()
            .AddSingleton<IConfiguration>(
                _ => new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .Build())
            .AddOptions();
    private readonly IServiceProvider _provider;
    public ServiceProviderClassFixture() {
        _provider = _services.BuildServiceProvider();
    }

    public T Service => _provider.GetRequiredService<T>();
}