using Estacionamento.Doamin.Helpers;
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
       var tabela = new TabelaDePreco {
       Data = DateTime.Now.Atual()
    };
        return View(tabela);
    }
}
