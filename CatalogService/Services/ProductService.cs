using CatalogService.Models;

namespace CatalogService.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(int page = 1, int size = 20);
    Task<Product?> GetAsync(int id);
    Task<int> CreateAsync(Product p);
}

public sealed class ProductService(IDbService db) : IProductService
{
    public Task<IEnumerable<Product>> GetAllAsync(int page = 1, int size = 20) =>
        db.QueryAsync<Product>(
            """
            SELECT Id, Name, Price, CreatedAt
            FROM dbo.Products
            ORDER BY Id DESC
            OFFSET (@page-1)*@size ROWS FETCH NEXT @size ROWS ONLY
            """, new { page, size });

    public Task<Product?> GetAsync(int id) =>
        db.QuerySingleAsync<Product>(
            "SELECT Id, Name, Price, CreatedAt FROM dbo.Products WHERE Id=@id", new { id });

    public Task<int> CreateAsync(Product p) =>
        db.ExecuteAsync(
            "INSERT INTO dbo.Products(Name, Price, CreatedAt) VALUES(@Name, @Price, SYSUTCDATETIME())",
            new { p.Name, p.Price });
}
