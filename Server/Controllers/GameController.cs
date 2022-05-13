using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Context;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class GameController : ControllerBase
{
    private readonly StoreContext _context;

    public GameController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<Game>> GetTModels()
    {
        return _context.Games;
    }

    [HttpPost("")]
    public ActionResult<Game> PostTModel(Game model)
    {
        _context.Games.Add(model);

        _context.SaveChanges();

        return model;
    }
}