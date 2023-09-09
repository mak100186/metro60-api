using Metro60.WebApi.Dependency;
using Metro60.WebApi.Health;
using Metro60.WebApi.Logging;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;

using Serilog;

Log.Logger = LoggingSetup.CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging()
    .UseHttpsRedirection()
    .UseRouting()
    .UseCors("MyAllowSpecificOrigins")
    .UseAuthentication()
    .UseAuthorization()
    .UseExceptionHandler("/error")
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/api/health", new HealthCheckOptions
        {
            ResponseWriter = HealthResponseFormatter.WriteResponse
        });
    });

app.Lifetime.ApplicationStarted.Register(() => Log.Information("Metro60 Products API - Started"));
app.Lifetime.ApplicationStopped.Register(() => Log.Information("Metro60 Products API - Stopped"));
app.Lifetime.ApplicationStopping.Register(() => Log.Information("Metro60 Products API - Stopping"));
Log.Information("Metro60 Products API - Starting");

await app.RunAsync();
