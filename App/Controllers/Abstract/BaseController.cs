using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Controllers.Abstract;

public class BaseController : Controller
{
    public ClaimsPrincipal claimUser = new ClaimsPrincipal();

    public BaseController()
    {
        claimUser = HttpContext?.User;
    }
}
