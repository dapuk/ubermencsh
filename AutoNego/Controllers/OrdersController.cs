using Microsoft.AspNetCore.Mvc;
using AutoNego.Models;
using AutoNego.Services;

namespace AutoNego.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController(IAutoNego svc) : ControllerBase
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
