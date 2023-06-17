using JwtAuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

/// One
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
/// Two
builder.Services.AddOcelot(builder.Configuration);

/// Four
/// Authentication & Authorization from ocelot api side
builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

/// Three
await app.UseOcelot();

/// Five
app.UseAuthentication();
app.UseAuthorization();

app.Run();
