using Estacionamento.Domain.Clientes;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Web.Controllers;

public class ClienteController : Controller
{
    private readonly IEstacionamentoContext _context;

    public ClienteController(IEstacionamentoContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(Buscar());
    }


    public IActionResult Create(int id = 0)
    {
        if (id != 0)
        {
            return View(Get(id));
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
                Save(cliente);
                TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Update(cliente);
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
            Delete(id);
            TempData["MensagemSucesso"] = "Item deletado com sucesso";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;

            return RedirectToAction("Index");
        }

    }

    public void Save(Cliente cliente)
    {
        try
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public void Update(Cliente cliente)
    {
        if (cliente.Id == 0)
        {
            throw new Exception("Erro inesperado ao atualizar Cliente");
        }
        else
        {
            _context.Clientes.Update(cliente);
        }
        _context.SaveChanges();
    }

    public Cliente Get(int id)
    {
        return _context.Clientes.First(q => q.Id == id);
    }

    public void Delete(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(q => q.Id == id);
        if (cliente == null)
            throw new Exception("Este cliente não existe");

        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
    }

    public ListCliente Buscar(int porPagina = 10, int paginaCorrente = 1)
    {
        var lista  = _context.Clientes.Skip((paginaCorrente - 1) * porPagina).Take(porPagina).ToList();
        var contador = _context.Clientes.Count();

        return new ListCliente
        {
            Clientes = lista,
            Contador = contador,
        };
    }
}
