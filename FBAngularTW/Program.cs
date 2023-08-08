using FBAngularTW;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ShortUrlService>();

var app = builder.Build();
app.UseShortUrl();
app.Run();
