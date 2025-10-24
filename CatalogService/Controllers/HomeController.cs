using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers;

public sealed class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index() => View();
}
