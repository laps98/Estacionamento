using Estacionamento.Domain.Context;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Web.Controllers;

public class AccountController : Controller
{
    private readonly IEstacionamentoContext _context;

    public AccountController(IEstacionamentoContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string login, string senha)
    {
        var usuario = _context.Usuarios.FirstOrDefault(q => q.Email == login);

        //if (usuario == null || !PasswordHelper.VerifyHashedPassword(usuario.Senha, request.Senha))
        if (usuario == null || (usuario.Senha != senha))
            return BadRequest(new { Message = "Usuário ou senha inválidos" });

        TempData["userId"] = usuario.Id;
        TempData["login"] = usuario.Email;

        HttpContext.Session.SetString("_UserId", usuario.Id.ToString());
        HttpContext.Session.SetString("_Login", usuario.Email.ToString());

        TempData["loginError"] = false;
        return RedirectToAction("Index", "Home");

        //return View();
    }

    public IActionResult Logout()
    {

        TempData["userId"] = null;
        TempData["login"] = null;

        HttpContext.Session.Remove("_UserId");
        HttpContext.Session.Remove("_Login");

        TempData["loginError"] = null;

        return Redirect("Login");
    }
}
  