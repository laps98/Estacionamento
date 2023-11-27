using Estacionamento.Domain.Context;
using Estacionamento.Domain.Usuarios;
using Estacionamento.Domain.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Web.Controllers;

[AllowAnonymous]
public class ContaController : Controller
{
    private readonly IEstacionamentoContext _context;
    private readonly IGerenciadorDeUsuario _gerenciador;

    public ContaController(IEstacionamentoContext context, IGerenciadorDeUsuario gerenciador)
    {
        _context = context;
        _gerenciador = gerenciador;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Index(LoginRequest request)
    {
        //HttpContext.SignInAsync();


        var usuario = _context.Usuarios.FirstOrDefault(q => q.Email == request.Login);

        //if (usuario == null || !PasswordHelper.VerifyHashedPassword(usuario.Senha, request.Senha))
        if (usuario == null || (usuario.Senha != request.Senha))
        {
            ModelState.Clear();
            ModelState.AddModelError("Login", "Usuário ou senha inválidos");
            return View(request);
        }

        //TempData["userId"] = usuario.Id;
        //TempData["login"] = usuario.Email;

        HttpContext.Session.SetString("_UserId", usuario.Id.ToString());
        HttpContext.Session.SetString("_Nome", usuario.Name.ToString());
        HttpContext.Session.SetString("_Login", usuario.Email.ToString());
        HttpContext.Session.SetString("_Admin", usuario.Administrador.ToString());

        TempData["loginError"] = false;
        return RedirectToAction("Index", "Home");

        //return View();
    }

    public IActionResult Logout()
    {

        //TempData["userId"] = null;
        //TempData["login"] = null;

        HttpContext.Session.Remove("_UserId");
        HttpContext.Session.Remove("_Nome");
        HttpContext.Session.Remove("_Login");
        HttpContext.Session.Remove("_Admin");

        TempData["loginError"] = null;

        return Redirect("Index");
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
                var usuarioDoBanco = _context.Usuarios.FirstOrDefault(q => q.Email == usuario.Email);
                if (usuarioDoBanco != null)
                {
                    TempData["MensagemErro"] = "E-mail já utilizado";
                    return View("Create", usuario);
                }
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


