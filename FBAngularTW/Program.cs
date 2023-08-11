using FBAngularTW;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RedirectionsOption>(
    builder.Configuration.GetSection("Redirections")
);

var app = builder.Build();

app.UseShortUrlRedirection();

app.Run();