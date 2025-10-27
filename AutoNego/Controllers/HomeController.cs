using Microsoft.AspNetCore.Mvc;

namespace AutoNego.Controllers;

public sealed class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index() => View();
}
