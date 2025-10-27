using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySqlConnector;

namespace CheckQuota.Health;

public class SqlHealthCheck(IConfiguration cfg) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = default)
    {
        var conn = cfg.GetConnectionString("MySqlServer");
        try
        {
            await using var c = new MySqlConnection(conn);
            await c.OpenAsync(ct);
            await using var cmd = new MySqlCommand("SELECT 1", c);
            await cmd.ExecuteScalarAsync(ct);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}
