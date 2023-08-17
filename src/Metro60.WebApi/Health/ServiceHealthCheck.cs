using Maxx.Plugin.Shared.Health;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Metro60.WebApi.Health;

/// <summary>
/// Service health check used by the pods to report status to Kubernetes
/// </summary>
public class ServiceHealthCheck : IHealthCheck
{
    private readonly IHealthProbe _healthProbe;
    private readonly ILogger<ServiceHealthCheck> _logger;

    public ServiceHealthCheck(ILogger<ServiceHealthCheck> logger, IHealthProbe healthProbe)
    {
        _logger = logger;
        _healthProbe = healthProbe;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;

            return _healthProbe.GetStatus();
        }
        catch (Exception ex)
        {
            _logger.LogError("Service health check failed with {@Exception}", ex);

            return HealthCheckResult.Unhealthy("Service is Unhealthy", ex);
        }
    }
}
