using App.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[Authorize]
public class HomeController : BaseController
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}