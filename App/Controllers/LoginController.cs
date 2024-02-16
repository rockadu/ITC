using App.Controllers.Abstract;
using App.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Service.Identificacao;
using System.Security.Claims;
using System.Text.Json;

namespace App.Controllers;

public class LoginController : BaseController
{
    private readonly IUsuarioService _usuarioService;

    public LoginController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser != null && claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginModel login)
    {
        var _usuario = await _usuarioService.Logar(login.Email, login.Senha);

        var a = JsonSerializer.Serialize(_usuario);

        if (_usuario != null)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, _usuario.Email),
                new Claim("Nome", $"{_usuario.Apelido}"),
                new Claim("Usuario", JsonSerializer.Serialize( _usuario)),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), properties);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Login");
    }
}