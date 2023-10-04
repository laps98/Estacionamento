using Estacionamento.Doamin.Helpers;
using Estacionamento.Domain.Clientes;
using Estacionamento.Domain.Context;
using Estacionamento.Domain.TabelasDePreco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Web.Controllers;

public class TabelaDePrecoController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeTabelaDePreco _tabelaDePreco;

    public TabelaDePrecoController(IEstacionamentoContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Create(int id = 0)
    {
        //if (id != 0)
        //{
        //    return View(Get(id));
        //}

        return View();
    }
    [HttpPost]
    public IActionResult Create(TabelaDePreco tabela)
    {
        try
        {
            if (tabela.Id == 0)
            {
                Save(tabela);
                TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                //Update(tabela);
                //TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                //return RedirectToAction("Index");
            }
            return View("Create", tabela);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex;
            return View("Create", tabela);
        }


    //    var tabela = new TabelaDePreco {
    //   Data = DateTime.Now.Atual()
    //};
    //    return View(tabela);
    }

    public void Save(TabelaDePreco tabela)
    {
        try
        {
            _context.TabelasDePreco.Add(tabela);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
}
