using Estacionamento.Doamin.Helpers;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.Pagination;
using Estacionamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Controllers;

public class HomeController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeMovimentacaoDeVeiculo _gerenciador;
    public HomeController(IEstacionamentoContext context, IGerenciadorDeMovimentacaoDeVeiculo gerenciadorDeVeiculo)
    {
        _context = context;
        _gerenciador = gerenciadorDeVeiculo;
    }

    public IActionResult Index(QueryFilter filter)
    {
        var hoje = DateTime.Now;
        var lista = _context.MovimentacoesDeVeiculo.AsNoTracking().Where(q => q.DataDeEntrada.Date == hoje.Date);
        var response = new ResponsePagination<MovimentacaoDeVeiculo>(filter).Buscar(lista, filter);

        Dropdown();
        var movimentacaoDeVeiculo = new MovimentacaoDeVeiculo
        {
            DataDeEntrada = DateTimeHelper.Atual(hoje)
        };

        ViewBag.ListagemDeVeiculos = response;

        return View(movimentacaoDeVeiculo);
    }
    public IActionResult BaixarDaHome(int id)
    {
        var movimentacaoDeVeiculo = _context.MovimentacoesDeVeiculo.First(q => q.Id == id);
        Dropdown(movimentacaoDeVeiculo);

        return RedirectToAction("CalcularDaHome", movimentacaoDeVeiculo);
    }
    public IActionResult Baixar(MovimentacaoDeVeiculo? movimentacaoDeVeiculo)
    {

        if (movimentacaoDeVeiculo != null && movimentacaoDeVeiculo.Id != 0)
        {
            Dropdown(movimentacaoDeVeiculo);

            movimentacaoDeVeiculo.DataDeEntrada = movimentacaoDeVeiculo.DataDeEntrada.HorarioDeBrasilia();
            return View(movimentacaoDeVeiculo);
        }
        Dropdown();
        return View();

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
    [HttpPost]
    public IActionResult Create(MovimentacaoDeVeiculo movimentacao)
    {
        try
        {
            if (movimentacao.Id == 0)
            {
                _gerenciador.Save(movimentacao);
                TempData["MensagemSucesso"] = "Movimentação cadastrada com sucesso";

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", movimentacao);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Home", movimentacao);
        }
    }
    [HttpGet]
    public IActionResult Calcular(string placa)
    {
        try
        {
            if (!placa.IsNullOrEmpty())
            {
                var movimentacao = _gerenciador.CalcularPorPlaca(placa);

                Dropdown(movimentacao);
                return View("Baixar", movimentacao);
            }

            return RedirectToAction("Baixar");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Baixar");
        }
    }
    public IActionResult CalcularDaHome(MovimentacaoDeVeiculo movimentacao)
    {
        try
        {
            if (movimentacao != null)
            {
                movimentacao = _gerenciador.CalcularPelaMovimentacaoDoVeiculo(movimentacao);

                Dropdown(movimentacao);
                return RedirectToAction("Baixar", movimentacao);
            }

            return RedirectToAction("Baixar");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Baixar");
        }
    }
    public IActionResult Delete(int id)
    {
        _gerenciador.Delete(id);

        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult BaixarMovimentacao(MovimentacaoDeVeiculo? movimentacao)
    {
        try
        {
            if (movimentacao != null && movimentacao.Id != 0)
            {
                _gerenciador.Baixar(movimentacao);
                TempData["MensagemSucesso"] = "Baixa executada com sucesso.";

                return RedirectToAction("Baixar");
            }

            return RedirectToAction("Baixar");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Baixar", movimentacao);
        }
    }
}