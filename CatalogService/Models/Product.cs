namespace CatalogService.Models;

public sealed class Product
{
    public int Id { get; init; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; init; }
}
