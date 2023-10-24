using Estacionamento.Domain.Usuarios;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Web.Controllers;

public class ClienteController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeUsuario _gerenciador;

    public ClienteController(IEstacionamentoContext context, IGerenciadorDeUsuario gerenciador)
    {
        _context = context;
        _gerenciador = gerenciador;
    }

    public IActionResult Index(QueryFilter filter)
    {
        var lista = _context.Clientes;

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
    public IActionResult Create(Usuario cliente)
    {
        try
        {
            if (cliente.Id == 0)
            {
                _gerenciador.Save(cliente);
                TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _gerenciador.Update(cliente);
                TempData["MensagemSucesso"] = "Cliente alterado com sucesso";
                return RedirectToAction("Index");
            }
            return View("Create", cliente);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;
            return View("Create", cliente);
        }
    }
}

