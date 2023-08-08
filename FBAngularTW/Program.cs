using FBAngularTW.ShortUrlLib;
using FBAngularTW.ShortUrlLib.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddShortUrl();

var app = builder.Build();

app.UseShortUrl();
app.Run();
