using System.Data;
using Dapper;
using AutoNego.Models;

namespace AutoNego.Services;

public interface IAutoNego
{
    Task<IEnumerable<BondTrx>> GetAllAsync();
    Task<int> CreateAsync(Req_Post_BondTrx req);
}

public sealed class AutoNego(IDbService db) : IAutoNego
{
    public async Task<IEnumerable<BondTrx>> GetAllAsync() =>
        await db.QueryAsync<BondTrx>("SELECT * FROM bond_trx ORDER BY id_bond_trx DESC");

    public async Task<int> CreateAsync(Req_Post_BondTrx req)
    {
        await using var con = await db.OpenAsync();
        await using var tx = await con.BeginTransactionAsync();
        try
        {
            var sql = @"
                      INSERT INTO bond_trx
                      (
                        side
                        ,series
                        ,counterparty_code
                        ,qty
                        ,price
                        ,yield
                        ,channel
                        ,status
                      )
                      VALUES(
                        @side
                        ,@series
                        ,@counterparty_code
                        ,@qty
                        ,@price
                        ,@yield
                        ,@channel
                        ,@status
                      );
                      ";
            var rows = await con.ExecuteAsync(sql, req, transaction: (IDbTransaction)tx);
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
