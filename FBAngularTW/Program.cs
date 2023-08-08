using FBAngularTW;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<List<RedirectionOption>>(
    builder.Configuration.GetSection("Redirections")
);

var app = builder.Build();

app.UseMiddleware<CustomRedirectionMiddleware>();

app.Run();