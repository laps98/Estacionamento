using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using Estacionamento.Domain.TabelasDePreco;
using Microsoft.AspNetCore.Mvc;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Web.Controllers;

public class TabelaDePrecoController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeTabelaDePreco _gerenciador;

    public TabelaDePrecoController(IEstacionamentoContext context, IGerenciadorDeTabelaDePreco gerenciador)
    {
        _context = context;
        _gerenciador = gerenciador;
    }

    public IActionResult Index(QueryFilter filter)
    {
        var lista =  _context.TabelasDePreco;
        var request = new ResponsePagination<TabelaDePreco>(filter).Buscar(lista, filter);

        return View();
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
    public IActionResult Create(TabelaDePreco tabela)
    {
        try
        {
            if (tabela.Id == 0)
            {
                _gerenciador.Save(tabela);
                TempData["MensagemSucesso"] = "Tabela de preço cadastrada com sucesso";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _gerenciador.Update(tabela);
                TempData["MensagemSucesso"] = "Tabela de preço alterada com sucesso";
                return RedirectToAction("Index");
            }
            return View("Create", tabela);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;
            return View("Create", tabela);
        }
    }
}
