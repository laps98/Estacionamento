using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using Estacionamento.Domain.TabelasDePreco;
using Estacionamento.Domain.Vagas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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
        var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));
        var lista =  _context.TabelasDePreco.Where(q => q.IdUsuario == idUsuario);
        var request = new ResponsePagination<TabelaDePreco>(filter).Buscar(lista, filter);

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
    public IActionResult Create(TabelaDePreco tabela)
    {
        try
        {
            if (tabela.Id == 0)
            {
                var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));
                tabela.IdUsuario = idUsuario;
                _gerenciador.Save(tabela);
                TempData["MensagemSucesso"] = "Tabela de preço cadastrada com sucesso";
                return RedirectToAction("Index");
            }
            ModelState.Remove("Usuario");
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
