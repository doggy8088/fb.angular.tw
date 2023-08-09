using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class ServiceProviderClassFixture<T> where T : class
{
    private readonly IServiceProvider _provider;
    public ServiceProviderClassFixture()
    {
        var builder = WebApplication.CreateBuilder();

        builder
            .Services
            .ConfigureOptions<ShortUrlConfigureOptions>();
        var app = builder.Build();
        _provider = app.Services.CreateScope().ServiceProvider; // IOptionsMonitor is scoped 
    }

    public T Service => _provider.GetRequiredService<T>();
}