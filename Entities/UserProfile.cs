using System.ComponentModel.DataAnnotations;

namespace Entities;

public class UserProfile
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
}