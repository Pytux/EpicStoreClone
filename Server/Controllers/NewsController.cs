using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Context;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class NewsController : ControllerBase
{
    private readonly StoreContext _context;
    private readonly ILogger<News> _logger;

    public NewsController(StoreContext context, ILogger<News> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<News>> GetTModels()
    {
        _logger.LogInformation("Returning News...");
        return _context.News;
    }

    [HttpPost("")]
    public ActionResult<News> PostUserProfile(News model)
    {
        _context.News.Add(model);

        _context.SaveChanges();

        return model;
    }
}