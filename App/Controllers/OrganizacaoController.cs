using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Organizacao;

namespace App.Controllers
{
    public class OrganizacaoController : Controller
    {
        private readonly IOrganizacaoService _organizacaoService;

        public OrganizacaoController(IOrganizacaoService organizacaoService)
        {
            _organizacaoService = organizacaoService;
        }

        public async Task<IActionResult> Setores(BaseListRequestDto request)
        {
            var _resultado = await _organizacaoService.ListarSetores(request);
            return View(_resultado);
        }

        public async Task<IActionResult> Cargos(BaseListRequestDto request)
        {
            var _resultado = await _organizacaoService.ListarCargos(request);
            return View(_resultado);
        }

        public async Task<IActionResult> Unidades(BaseListRequestDto request)
        {
            var _resultado = await _organizacaoService.ListarUnidades(request);
            return View(_resultado);
        }
    }
}