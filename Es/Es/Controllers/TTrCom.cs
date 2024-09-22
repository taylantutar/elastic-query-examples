using Microsoft.AspNetCore.Mvc;

namespace Es.Controllers;

public class TTrCom : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}