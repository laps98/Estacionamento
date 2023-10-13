using Estacionamento.Doamin.Helpers;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.Pagination;
using Estacionamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Estacionamento.Domain.Pagination.PaginationHelper;

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

    public IActionResult Index(QueryFilter filter)
    {
        var lista = _context.MovimentacoesDeVeiculo.AsNoTracking();
        var request = new ResponsePagination<MovimentacaoDeVeiculo>(filter).Buscar(lista, filter);

        Dropdown();
        var hoje = DateTime.Now;
        var movimentacaoDeVeiculo = new MovimentacaoDeVeiculo
        {
            DataDeEntrada = DateTimeHelper.Atual(hoje)
        };

        ViewBag.ListagemDeVeiculos = request;

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