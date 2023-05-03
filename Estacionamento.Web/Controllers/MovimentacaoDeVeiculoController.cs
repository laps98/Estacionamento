using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Web.Controllers;

public class MovimentacaoDeVeiculoController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Create() => View();
}
