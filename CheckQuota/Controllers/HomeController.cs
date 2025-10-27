using Microsoft.AspNetCore.Mvc;

namespace CheckQuota.Controllers;

public sealed class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index() => View();
}
