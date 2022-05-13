using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Context;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserGameController : ControllerBase
{
    private readonly StoreContext _context;

    public UserGameController(StoreContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }


    [HttpGet("GetGames/{id}")]
    public ActionResult<IEnumerable<Game>> GetGamesByUser(string id)
    {
        var gameList = _context.UserGames.Where(p => p.UserProfileId == Guid.Parse(id)).Select(p => p.GameId).ToList();

        return _context.Games.Where(p => gameList.Contains(p.Id)).ToList();
    }

    [HttpPost("")]
    public ActionResult<UserGames> PostTModel(UserGames model)
    {
        _context.UserGames.Add(model);

        _context.SaveChanges();

        return model;
    }
}