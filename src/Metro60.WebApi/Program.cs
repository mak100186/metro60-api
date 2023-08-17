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

await app.RunAsync();
