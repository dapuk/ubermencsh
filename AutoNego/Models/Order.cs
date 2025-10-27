namespace OrderService.Models;

public sealed class Order
{
    public int Id { get; init; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; init; }
}
