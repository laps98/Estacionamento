using Estacionamento.Domain.Context;
using Estacionamento.Domain.MovimentacoesDeCaixa;
using Estacionamento.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Web.Controllers;

public class MovimentacaoDeCaixaController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeMovimentacaoDeCaixa _gerenciador;

    public MovimentacaoDeCaixaController(IGerenciadorDeMovimentacaoDeCaixa gerenciador, IEstacionamentoContext context)
    {
        _gerenciador = gerenciador;
        _context = context;
    }

    public IActionResult Index(QueryFilter filter)
    {
        var lista = _context.MovimentacoesDeCaixa;

        var request = new ResponsePagination<MovimentacaoDeCaixa>(filter).Buscar(lista, filter);

        return View(request);
    }

    public IActionResult Create(int id = 0)
    {
        if (id != 0)
        {
            return View(_gerenciador.Get(id));
        }

        return View();
    }

    [HttpPost]
    public IActionResult Create(MovimentacaoDeCaixa movimentacao)
    {
        try
        {
            if (movimentacao.Id == 0)
            {
                var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));
                movimentacao.IdUsuario = idUsuario;
                _gerenciador.Save(movimentacao);
                TempData["MensagemSucesso"] = "Movimentação cadastrada com sucesso";
                return RedirectToAction("Index");
            }
            ModelState.Remove("Usuario");
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
}
