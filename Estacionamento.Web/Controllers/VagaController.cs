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
        var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));
        var lista = _context.Vagas.Where(q => q.IdUsuario == idUsuario);
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
    public IActionResult Create(Vaga vaga)
    {
        try
        {
            if (vaga.Id == 0)
            {
                var idUsuario = int.Parse(HttpContext.Session.GetString("_UserId"));
                vaga.IdUsuario = idUsuario;
                _gerenciador.Save(vaga);
                TempData["MensagemSucesso"] = "Vaga cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            ModelState.Remove("Usuario");
            if (ModelState.IsValid)
            {
                _gerenciador.Update(vaga);
                TempData["MensagemSucesso"] = "Vaga alterado com sucesso";
                return RedirectToAction("Index");
            }
            return View("Create", vaga);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;
            return View("Create", vaga);
        }
    }
}
