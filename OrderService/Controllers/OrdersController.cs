using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController(IOrderService svc) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> Get() => Ok(await svc.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Order o)
    {
        if (o.ProductId <= 0 || o.Quantity <= 0 || o.TotalAmount <= 0) return BadRequest("Invalid payload");
        await svc.CreateAsync(o);
        return Accepted();
    }
}
