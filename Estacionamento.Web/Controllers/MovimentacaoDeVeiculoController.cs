using Estacionamento.Domain.Context;
using Microsoft.AspNetCore.Mvc;

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
        var tabelaDePreco = _context.TabelasDePreco.Select(q => new
        {
            q.Id,
            q.Descricao
        });

        ViewBag.Tabela = new
        {
            TabelaDePreco = tabelaDePreco
        };

        return View();
    }

}
