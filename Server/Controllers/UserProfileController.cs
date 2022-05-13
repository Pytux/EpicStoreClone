using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Context;

namespace BlazorStore.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserProfileController : ControllerBase
{
    private readonly StoreContext _context;

    public UserProfileController(StoreContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<UserProfile>> GetUserProfiles()
    {
        return _context.UserProfiles;
    }

    [HttpGet("{id}")]
    public ActionResult<UserProfile> GetUserProfileById(string id)
    {
        return _context.UserProfiles.FirstOrDefault(p => p.Id == Guid.Parse(id));
    }

    [HttpPost("")]
    public ActionResult<UserProfile> PostUserProfile(UserProfile model)
    {
        _context.UserProfiles.Add(model);

        _context.SaveChanges();

        return model;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserProfile(string id, UserProfile model)
    {
        var user = _context.UserProfiles.FirstOrDefault(p => p.Id == Guid.Parse(id));

        if (user != null)
        {
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Country = model.Country;
            user.Email = model.Email;

            await _context.SaveChangesAsync();

            return Ok();
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public ActionResult<UserProfile> DeleteUserProfileById(int id)
    {
        return null;
    }
}