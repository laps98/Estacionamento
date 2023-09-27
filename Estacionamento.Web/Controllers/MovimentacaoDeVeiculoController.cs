using Estacionamento.Domain.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Estacionamento.Web.Controllers;

public class MovimentacaoDeVeiculoController : Controller
{
    private readonly IEstacionamentoContext _context;

    public MovimentacaoDeVeiculoController(IEstacionamentoContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View();
    public IActionResult Create()
    {
        var tabelaDePreco = _context.TabelasDePreco.ToList();

        ViewBag.TabelaDePreco = new SelectList(tabelaDePreco, "Id", "Descricao", null);

        return View();
    }

}
