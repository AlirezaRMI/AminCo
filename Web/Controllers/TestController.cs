using Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class TestController(AmincoDbContext context) : Controller
{
    public IActionResult Index()
    {
        var canConnect = context.Database.CanConnect();
        return Content($"Database connection: {(canConnect ? "OK" : "Failed")}");
    }
}