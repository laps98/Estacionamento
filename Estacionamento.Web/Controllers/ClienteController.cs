using Estacionamento.Domain.Clientes;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Web.Controllers;

public class ClienteController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeCliente _gerenciador;

    public ClienteController(IEstacionamentoContext context, IGerenciadorDeCliente gerenciador)
    {
        _context = context;
        _gerenciador = gerenciador;
    }

    public IActionResult Index(QueryFilter filter)
    {
        var lista = _context.Clientes;

        var request = new ResponsePagination<Cliente>(filter).Buscar(lista, filter);

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
    public IActionResult Create(Cliente cliente)
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

