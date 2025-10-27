using System.Data;
using Dapper;
using AutoNego.Models;

namespace AutoNego.Services;

public interface IAutoNego
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<int> CreateAsync(Order o);
}

public sealed class AutoNego(IDbService db) : IAutoNego
{
    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await db.QueryAsync<Order>("SELECT * FROM dbo.Orders ORDER BY Id DESC");

    public async Task<int> CreateAsync(Order o)
    {
        await using var con = await db.OpenAsync();
        await using var tx = await con.BeginTransactionAsync();
        try
        {
            var sql = """
                      INSERT INTO dbo.Orders(ProductId, Quantity, TotalAmount, CreatedAt)
                      VALUES(@ProductId, @Quantity, @TotalAmount, SYSUTCDATETIME());
                      """;
            var rows = await con.ExecuteAsync(sql, o, transaction: (IDbTransaction)tx);
            await tx.CommitAsync();
            return rows;
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}
