using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers;

public sealed class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index() => View();
}
