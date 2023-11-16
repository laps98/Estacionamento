using Estacionamento.Domain.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Web.Controllers;

[AllowAnonymous]
public class ContaController : Controller
{
    private readonly IEstacionamentoContext _context;

    public ContaController(IEstacionamentoContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Index(LoginRequest request)
    {
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

        TempData["loginError"] = null;

        return Redirect("Login");
    }
}


public sealed record LoginRequest
{
    public string Login { get; set; }
    public string Senha { get; set; }
}