using Estacionamento.Domain.Clientes;
using Estacionamento.Domain.Context;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Web.Controllers;

public class ClienteController : Controller
{
    private readonly IClienteApi _api;

    public ClienteController(IClienteApi api)
    {
        _api = api;
    }

    public IActionResult Index()
    {
        return View(_api.List());
    }


    public IActionResult Create(int id = 0)
    {
        if (id != 0)
        {
            return View(_api.Get(id));
        }

        return View();
    }

    [HttpPost]
    public IActionResult Create(Cliente cliente)
    {
        try
        {
            if(cliente.Id == 0)
            {
                _api.Save(cliente);
                TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _api.Update(cliente);
                TempData["MensagemSucesso"] = "Contato alterado com sucesso";
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

    public IActionResult Deletar(int id)
    {
        try
        {
            _api.Delete(id);
            TempData["MensagemSucesso"] = "Item deletado com sucesso";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;

            return RedirectToAction("Index");
        }

    }
}
