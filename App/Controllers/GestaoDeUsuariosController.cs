using App.Controllers.Abstract;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Identificacao;

namespace App.Controllers;

public class GestaoDeUsuariosController : BaseController
{
    private readonly IUsuarioService _usuarioService;

    public GestaoDeUsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task<IActionResult> Listar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ListarUsuarios(BaseListRequestDto request)
    {
        return View(await _usuarioService.Listar(request));
    }

    [HttpGet("{codigo}")]
    public async Task<IActionResult> Usuario(int codigo)
    {
        return View();
    }

    public async Task<IActionResult> Atualizar()
    {
        return View();
    }

    public async Task<IActionResult> Adicionar()
    {
        return View();
    }
}