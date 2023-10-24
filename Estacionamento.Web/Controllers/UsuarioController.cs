using Estacionamento.Domain.Usuarios;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Web.Controllers;

public class UsuarioController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeUsuario _gerenciador;

    public UsuarioController(IEstacionamentoContext context, IGerenciadorDeUsuario gerenciador)
    {
        _context = context;
        _gerenciador = gerenciador;
    }

    public IActionResult Index(QueryFilter filter)
    {
        var lista = _context.Usuarios;

        var request = new ResponsePagination<Usuario>(filter).Buscar(lista, filter);

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
    public IActionResult Create(Usuario usuario)
    {
        try
        {
            if (usuario.Id == 0)
            {
                _gerenciador.Save(usuario);
                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _gerenciador.Update(usuario);
                TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
                return RedirectToAction("Index");
            }
            return View("Create", usuario);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;
            return View("Create", usuario);
        }
    }
}

