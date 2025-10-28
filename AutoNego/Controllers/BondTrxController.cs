using AutoNego.Models;
using AutoNego.Services;
using AutoNego.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;

namespace AutoNego.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BondTrxController(IAutoNego svc) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BondTrx>>> Get() => Ok(await svc.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Req_Post_BondTrx req)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .SelectMany(kvp => kvp.Value.Errors
                    .Select(error => new
                    {
                        field = kvp.Key,
                        error = error.ErrorMessage
                    }))
                .ToList();

            return BadRequest(errors);
        }
        else
        {
            string param = string.IsNullOrEmpty(req.series) ? null : $"?series={req.series}";
            var resultSeries = await Helper.HitMicroServices_Get("CheckQuota", "GetSeries", param);
            string msgSeries = resultSeries.Content.ReadAsStringAsync().Result;
            var getSeries = JsonConvert.DeserializeObject<QuotaSeries>(msgSeries);
            if (getSeries is not null)
            {
                try
                {
                    dynamic reqUpdate = new ExpandoObject();
                    reqUpdate.series = req.series;
                    reqUpdate.qty = req.qty;

                    // Jika side nya B, maka qty akan berkurang
                    if (req.side.ToLower() == "b")
                    {
                        await Helper.HitMicroServices_Post("CheckQuota", "AddQuota", reqUpdate);
                    }
                    else if (req.side.ToLower() == "s")
                    {
                        // Jika side nya S, maka qty akan bertambah
                        await Helper.HitMicroServices_Post("CheckQuota", "MinusQuota", reqUpdate);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            isError = true,
                            message = $"Side {req.side} not found"
                        });
                    }


                    await svc.CreateAsync(req);
                    return Ok(new
                    {
                        isError = false,
                        message = $"Bond Trx inserted successfully {req.series}"
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        isError = true,
                        message = $"Bond Trx inserted failure, error: {ex.Message}"
                    });
                }
            }
            else
            {
                return NotFound(new
                {
                    isError = true,
                    message = $"Series {req.series} not found"
                });
            }
        }
    }
}
