using Estacionamento.Domain.TabelasDePreco;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Web.Controllers;

public class TabelaDePrecoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {

        return View(new TabelaDePreco());
    }
}
