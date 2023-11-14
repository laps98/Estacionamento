using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using Estacionamento.Domain.Vagas;
using Microsoft.AspNetCore.Mvc;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Web.Controllers;

public class VagaController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeVaga _gerenciador;

    public VagaController(IEstacionamentoContext context, IGerenciadorDeVaga gerenciador)
    {
        _context = context;
        _gerenciador = gerenciador;
    }

    public IActionResult Index(QueryFilter filter)
    {
        var lista = _context.Vagas;

        var request = new ResponsePagination<Vaga>(filter).Buscar(lista, filter);

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
    public IActionResult Create(Vaga Vaga)
    {
        try
        {
            if (Vaga.Id == 0)
            {
                _gerenciador.Save(Vaga);
                TempData["MensagemSucesso"] = "Vaga cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _gerenciador.Update(Vaga);
                TempData["MensagemSucesso"] = "Vaga alterado com sucesso";
                return RedirectToAction("Index");
            }
            return View("Create", Vaga);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;
            return View("Create", Vaga);
        }
    }
}
