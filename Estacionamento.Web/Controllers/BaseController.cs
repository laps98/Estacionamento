using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Web.Controllers;

public class BaseController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
