using Estacionamento.Doamin.Helpers;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.Pagination;
using Estacionamento.Models;
using Estacionamento.Web.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Controllers;

[CheckSessionIsAvailable]
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
        var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));
        var lista = _context.MovimentacoesDeVeiculo.AsNoTracking().Where(q => q.DataDeEntrada.Date == hoje.Date && q.IdUsuario == idUsuario);
        var response = new ResponsePagination<MovimentacaoDeVeiculo>(filter).Buscar(lista, filter);

        DropdownTabelaDePreco(idUsuario);
        DropdownVaga(idUsuario);
        var movimentacaoDeVeiculo = new MovimentacaoDeVeiculo
        {
            DataDeEntrada = DateTimeHelper.Atual(hoje)
        };

        ViewBag.ListagemDeVeiculos = response;

        return View(movimentacaoDeVeiculo);
    }
    public IActionResult BaixarDaHome(int id)
    {
        var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));

        var movimentacaoDeVeiculo = _context.MovimentacoesDeVeiculo.First(q => q.Id == id);
        DropdownTabelaDePreco(idUsuario, movimentacaoDeVeiculo);
        DropdownVaga(idUsuario, movimentacaoDeVeiculo);

        return RedirectToAction("CalcularDaHome", movimentacaoDeVeiculo);
    }
    public IActionResult Baixar(MovimentacaoDeVeiculo? movimentacaoDeVeiculo)
    {
        var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));

        if (movimentacaoDeVeiculo != null && movimentacaoDeVeiculo.Id != 0)
        {
            DropdownTabelaDePreco(idUsuario, movimentacaoDeVeiculo);
            DropdownVaga(idUsuario, movimentacaoDeVeiculo);

            return View(movimentacaoDeVeiculo);
        }
        DropdownTabelaDePreco(idUsuario);
        DropdownVaga(idUsuario);
        return View();

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
                var hoje = DateTime.Now;
                var movimentacaoDoBanco = _context.MovimentacoesDeVeiculo;

                var vagaOcupada = movimentacaoDoBanco.FirstOrDefault(q =>
                    q.DataDeEntrada.Date == hoje.Date &&
                    q.DataDeSaida == null &&
                    q.IdVaga == movimentacao.IdVaga);
                var placaJaRegistrada = movimentacaoDoBanco.FirstOrDefault(q =>
                    q.DataDeEntrada.Date == hoje.Date &&
                    q.DataDeSaida == null &&
                    q.Placa == movimentacao.Placa);
                if (vagaOcupada != null)
                {
                    TempData["MensagemErro"] = "Esta vaga já está ocupada";
                    return RedirectToAction("Index", movimentacao);
                }
                if (placaJaRegistrada != null)
                {
                    TempData["MensagemErro"] = "Esta placa já está registrada";
                    return RedirectToAction("Index", movimentacao);
                }


                var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));
                movimentacao.IdUsuario = idUsuario;
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
            var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));

            if (!placa.IsNullOrEmpty())
            {
                var movimentacao = _gerenciador.CalcularPorPlaca(placa);

                DropdownTabelaDePreco(idUsuario,movimentacao);
                DropdownVaga(idUsuario,movimentacao);
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
    private void DropdownTabelaDePreco(int idUsuario, MovimentacaoDeVeiculo? movimentacaoDeVeiculo = null)
    {
        var items = _context.TabelasDePreco.Where(q => q.IdUsuario == idUsuario).Select(q => new { q.Id, q.Descricao });
        ViewBag.TabelaDePreco = new SelectList(items, "Id", "Descricao", movimentacaoDeVeiculo?.IdTabelaDePreco);
    }
    private void DropdownVaga(int idUsuario, MovimentacaoDeVeiculo? movimentacaoDeVeiculo = null)
    {
        var items = _context.Vagas.Where(q => q.IdUsuario == idUsuario).Select(q => new { q.Id, q.Nome });
        ViewBag.Vagas = new SelectList(items, "Id", "Nome", movimentacaoDeVeiculo?.IdVaga);
    }
    public IActionResult Delete(int id)
    {
        _gerenciador.Delete(id);

        return RedirectToAction("Index");
    }
    public IActionResult CalcularDaHome(MovimentacaoDeVeiculo movimentacao)
    {
        try
        {
            var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));

            if (movimentacao != null)
            {
                movimentacao = _gerenciador.CalcularPelaMovimentacaoDoVeiculo(movimentacao);

                DropdownTabelaDePreco(idUsuario, movimentacao);
                DropdownVaga(idUsuario, movimentacao);
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
}