using CatalogService.Models;
using CatalogService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController(IProductService svc) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get([FromQuery] int page = 1, [FromQuery] int size = 20) =>
        Ok(await svc.GetAllAsync(page, size));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetOne(int id)
        => (await svc.GetAsync(id)) is { } p ? Ok(p) : NotFound();

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Product p)
    {
        if (string.IsNullOrWhiteSpace(p.Name)) return BadRequest("Name required");
        await svc.CreateAsync(p);
        return CreatedAtAction(nameof(GetOne), new { id = p.Id }, null);
    }
}
