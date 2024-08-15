using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Controllers.Abstract;

public class BaseController : Controller
{
    public BaseController()
    {
    }
    protected string GetCurrentUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    protected string GetCurrentUserName()
    {
        return User.Claims.FirstOrDefault(x => x.Type.Equals("Usuario")).ToString();
    }
}
