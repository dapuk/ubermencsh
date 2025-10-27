using CheckQuota.Models;

namespace CheckQuota.Services;

public interface IQuotaService
{
    Task<IEnumerable<QuotaSeries>> GetAllAsync(int page = 1, int size = 20);
    Task<QuotaSeries?> GetAsync(int id);
    Task<QuotaSeries?> GetBySeriesAsync(string series);
    Task<QuotaSeries?> CheckingQuotaAsync(int quota, int id);
    Task<int> CreateAsync(Req_QuotaSeries p);
    Task<int> UpdateAsync(Req_Update_QuotaSeries p);
    Task<int> DeleteAsync(int id);
}

public sealed class QuotaService(IDbService db) : IQuotaService
{
    public Task<IEnumerable<QuotaSeries>> GetAllAsync(int page = 1, int size = 20) =>
        db.QueryAsync<QuotaSeries>(
            @"
            SELECT *
            FROM quota_series
            ORDER BY id_quota_series DESC
            OFFSET (@page-1)*@size ROWS FETCH NEXT @size ROWS ONLY
            ", new { page, size });

    public Task<QuotaSeries?> GetAsync(int id) =>
        db.QuerySingleAsync<QuotaSeries>(
            "SELECT * FROM quota_series WHERE id_quota_series=@id", new { id });

    public Task<QuotaSeries?> GetBySeriesAsync(string series) =>
        db.QuerySingleAsync<QuotaSeries>(
            "SELECT * FROM quota_series WHERE series=@series", new { series });

    public Task<QuotaSeries?> CheckingQuotaAsync(int quota, int id) =>
        db.QuerySingleAsync<QuotaSeries>(
            "SELECT * FROM quota_series WHERE @quota<=quota AND id_quota_series=@id", new { quota, id });

    public Task<int> CreateAsync(Req_QuotaSeries p) =>
        db.ExecuteAsync(
            "INSERT INTO quota_series(series, quota) VALUES(@series, @quota)",
            new { p.series, p.quota });

    public Task<int> UpdateAsync(Req_Update_QuotaSeries p) =>
        db.ExecuteAsync(
            "UPDATE quota_series SET series=@series, quota=@quota WHERE id_quota_series=@id_quota_series",
            new { p.series, p.quota, p.id_quota_series });
    public Task<int> DeleteAsync(int id) =>
        db.ExecuteAsync(
            "DELETE FROM quota_series WHERE id_quota_series=@id",
            new { id });
}
