﻿using App.Controllers.Abstract;
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

    [HttpGet]
    public async Task<IActionResult> Listar(BaseListRequestDto request)
    {
        return View(await _usuarioService.Listar(request));
    }

    [HttpGet]
    public async Task<IActionResult> GerarTemplateExcelUsuarios()
    {
        return Ok();
    }

    //[HttpPost]
    //public async Task<ActionResult> Upload(IFormFile file)
    //{
    //    try
    //    {
    //        if (file.Length > 0)
    //        {
    //            await _usuarioService.ImportarExcel(file, dataVirada);

    //            return Json(new ResultModel(), JsonRequestBehavior.AllowGet);
    //        }
    //        return View();
    //    }
    //    catch (Exception ex)
    //    {
    //        return Json(new ResultModel().Fail(ex.Message), JsonRequestBehavior.AllowGet);
    //    }
    //}
}