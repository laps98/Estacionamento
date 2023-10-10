using Estacionamento.Doamin.Helpers;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Estacionamento.Controllers;

public class HomeController : Controller
{
    private readonly IEstacionamentoContext _context;

    public HomeController(IEstacionamentoContext context)
    {
        _context = context;
    }

    //public HomeController(ILogger<HomeController> logger)
    //{
    //    _logger = logger;
    //}

    public IActionResult Index()
    {
        Dropdown();
        var hoje = DateTime.Now;
        var movimentacaoDeVeiculo = new MovimentacaoDeVeiculo
        {
            DataDeEntrada = DateTimeHelper.Atual(hoje)
        };
        return View(movimentacaoDeVeiculo);
    }
    private void Dropdown(MovimentacaoDeVeiculo? movimentacaoDeVeiculo = null)
    {
        var items = _context.TabelasDePreco.Select(q => new { q.Id, q.Descricao });
        ViewBag.TabelaDePreco = new SelectList(items, "Id", "Descricao", movimentacaoDeVeiculo?.IdTabelaDePreco);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}