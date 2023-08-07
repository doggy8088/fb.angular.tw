using FBAngularTW;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

app.Run(ctx =>
{
	var defaultTargetUrl = app.Configuration.GetValue<string>("DefaultTargetUrl");

	var mappings = app.Configuration.GetSection("ShortUrlMappings").Get<ShortUrlMapping[]>();
	if (mappings == null)
	{
		ctx.Response.Redirect(defaultTargetUrl);
		return Task.CompletedTask;
	}

	var mapping = mappings.FirstOrDefault(x => string.Compare(x.ShortUrl, ctx.Request.Host.Host) == 0);
	if (mapping == null)
	{
		ctx.Response.Redirect(defaultTargetUrl);
	}
	else
	{
		ctx.Response.Redirect(mapping.TargetUrl);
	}

	return Task.CompletedTask;
});

app.Run();
