using CheckQuota.Models;
using CheckQuota.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheckQuota.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class QuotasController(IQuotaService svc) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuotaSeries>>> Get([FromQuery] int page = 1, [FromQuery] int size = 20) =>
        Ok(await svc.GetAllAsync(page, size));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuotaSeries>> GetOne(int id)
        => (await svc.GetAsync(id)) is { } p ? Ok(p) : NotFound();

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Req_QuotaSeries p)
    {
        if (string.IsNullOrWhiteSpace(p.series)) return BadRequest("Series required");
        await svc.CreateAsync(p);
        return Ok();
    }

    [HttpPost("Checking")]
    public async Task<ActionResult> Checking([FromBody] Req_QuotaSeries p)
    {
        if (string.IsNullOrWhiteSpace(p.series)) return BadRequest("Series required");
        var quota = (await svc.GetBySeriesAsync(p.series));
        if (quota is null)
        {
            return NotFound($"Data Series not found [{p.series}]");
        }
        else
        {
            var getQuota = await svc.CheckingQuotaAsync(p.quota, quota.id_quota_series);
            if (getQuota is null)
            {
                return NotFound($"Quota under stock with series [{p.series}] and quota [{p.quota}] from [{quota.quota}]");
            }
            else
            {
                return Ok("Quota Breach");
            }
        }
    }
}
