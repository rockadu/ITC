﻿using Domain.Models;
using Domain.Models.Organizacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Organizacao;

namespace App.Controllers;

[Authorize]
public class OrganizacaoController : Controller
{
    private readonly IOrganizacaoService _organizacaoService;

    public OrganizacaoController(IOrganizacaoService organizacaoService)
    {
        _organizacaoService = organizacaoService;
    }

    public async Task<IActionResult> Setores(BaseListRequestModel request)
    {
        var _resultado = await _organizacaoService.ListarSetores(request);
        return View(_resultado);
    }

    public async Task<IActionResult> Cargos(BaseListRequestModel request)
    {
        var _resultado = await _organizacaoService.ListarCargos(request);
        return View(_resultado);
    }

    public async Task<IActionResult> Unidades(BaseListRequestModel request)
    {
        var _resultado = await _organizacaoService.ListarUnidades(request);
        return View(_resultado);
    }

    public async Task<IActionResult> AdicionarUnidade([FromBody] AdicionarUnidadeModel request)
    {
        await _organizacaoService.AdicionarUnidadeAsync(request);
        return Ok();
    }

    public async Task<IActionResult> UnidadesSelectList()
    {
        return Json(await _organizacaoService.UnidadesSelectList());
    }

    public async Task<IActionResult> SetoresPorUnidadeSelectList([FromQuery] int codigoUnidade)
    {
        return Json(await _organizacaoService.SetoresPorUnidadeSelectList(codigoUnidade));
    }

    public async Task<IActionResult> CargosSelectList()
    {
        return Json(await _organizacaoService.CargosSelectList());
    }

    public async Task<IActionResult> ExportarExcelUnidades()
    {
        var _arquivo = await _organizacaoService.ExportarExcelUnidades();

        return File(_arquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Unidades{DateTime.Now:yyyy-MM-dd HH:mm:ss}.xlsx");
    }

    public async Task<IActionResult> ExportarExcelSetores()
    {
        var _arquivo = await _organizacaoService.ExportarExcelUnidades();

        return File(_arquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Setores{DateTime.Now:yyyy-MM-dd HH:mm:ss}.xlsx");
    }

    public async Task<IActionResult> ExportarExcelCargos()
    {
        var _arquivo = await _organizacaoService.ExportarExcelUnidades();

        return File(_arquivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Cargos{DateTime.Now:yyyy-MM-dd HH:mm:ss}.xlsx");
    }

    public async Task<IActionResult> ImportarExcelUnidades(IFormFile excelFile)
    {
        if (excelFile == null || excelFile.Length == 0)
        {
            return BadRequest("Nenhum arquivo foi enviado.");
        }

        using (var stream = new MemoryStream())
        {
            await excelFile.CopyToAsync(stream);
            stream.Position = 0;
            await _organizacaoService.ImportarExcelUnidades(stream);
        }

        return Ok("Importação concluída com sucesso.");
    }

    [HttpPost]
    public async Task<IActionResult> InativarUnidades([FromBody] int[] Unidades)
    {
        await _organizacaoService.InativarUnidadesRangeAsync(Unidades);

        return Ok();
    }
}