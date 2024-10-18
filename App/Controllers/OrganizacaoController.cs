using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class OrganizacaoController : Controller
    {
        public IActionResult Setores()
        {
            return View();
        }

        public IActionResult Cargos()
        {
            return View();
        }

        public IActionResult Unidades()
        {
            return View();
        }
    }
}