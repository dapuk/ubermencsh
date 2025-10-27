using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AutoNego.Services;

public interface IDbService
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CommandType type = CommandType.Text);
    Task<T?> QuerySingleAsync<T>(string sql, object? param = null, CommandType type = CommandType.Text);
    Task<int> ExecuteAsync(string sql, object? param = null, CommandType type = CommandType.Text);
    Task<SqlConnection> OpenAsync();
}

public sealed class DbService(IConfiguration cfg) : IDbService
{
    private readonly string _conn = cfg.GetConnectionString("SqlServer")
        ?? Environment.GetEnvironmentVariable("ConnectionStrings__SqlServer")
        ?? throw new InvalidOperationException("Missing connection string.");

    public async Task<SqlConnection> OpenAsync()
    {
        var con = new SqlConnection(_conn);
        await con.OpenAsync();
        return con;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CommandType type = CommandType.Text)
    {
        await using var con = new SqlConnection(_conn);
        return await con.QueryAsync<T>(sql, param, commandType: type);
    }

    public async Task<T?> QuerySingleAsync<T>(string sql, object? param = null, CommandType type = CommandType.Text)
    {
        await using var con = new SqlConnection(_conn);
        return await con.QuerySingleOrDefaultAsync<T>(sql, param, commandType: type);
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null, CommandType type = CommandType.Text)
    {
        await using var con = new SqlConnection(_conn);
        return await con.ExecuteAsync(sql, param, commandType: type);
    }
}
