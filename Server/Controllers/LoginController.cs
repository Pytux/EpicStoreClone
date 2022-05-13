using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Context;
using Server.Helpers;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class LoginController : ControllerBase
{
    private readonly StoreContext _context;
    private readonly TokenHelper _tokeHelper;

    public LoginController(TokenHelper tokeHelper, StoreContext context)
    {
        _tokeHelper = tokeHelper;
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<string> LoginUser([FromBody] UserLogin user)
    {
        var userProfile =
            _context.UserProfiles.FirstOrDefault(p => p.UserName == user.Email && p.Password == p.Password);

        if (userProfile != null)
            return _tokeHelper.GenerateToken(userProfile.UserName, userProfile.Id.ToString());

        return Unauthorized();
    }
}