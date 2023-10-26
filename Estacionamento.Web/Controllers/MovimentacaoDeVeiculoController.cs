using Estacionamento.Domain.Context;
using Estacionamento.Domain.MovimentacoesDeVeiculo;
using Estacionamento.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Web.Controllers;
public class MovimentacaoDeVeiculoController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeMovimentacaoDeVeiculo _gerenciador;
    public MovimentacaoDeVeiculoController(IEstacionamentoContext context, IGerenciadorDeMovimentacaoDeVeiculo gerenciador)
    {
        _context = context;
        _gerenciador = gerenciador;
    }
    public IActionResult Index(QueryFilter filter)
    {
        var lista = _context.MovimentacoesDeVeiculo;

        var request = new ResponsePagination<MovimentacaoDeVeiculo>(filter).Buscar(lista, filter);

        return View(request);
    }
    public IActionResult Create(int id = 0)
    {
        if (id != 0)
        {
            var movimentacaoDeVeiculo = _gerenciador.Get(id);
            Dropdown(movimentacaoDeVeiculo);

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
            if (ModelState.IsValid)
            {
                _gerenciador.Update(movimentacao);
                TempData["MensagemSucesso"] = "Movimentação alterada com sucesso";
                return RedirectToAction("Index");
            }
            return View("Create", movimentacao);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;
            return View("Create", movimentacao);
        }
    }

    [HttpGet]
    public IActionResult Calcular(string placa)
    {
        var movimentacao = _gerenciador.Calcular(placa);
        return ViewBag.Movimentacao = movimentacao;
    }
}
