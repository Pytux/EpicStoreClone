using System.ComponentModel.DataAnnotations;

namespace Entities;

public class UserGames
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserProfileId { get; set; }
    public Guid GameId { get; set; }

    public UserProfile UserProfile { get; set; }
    public Game Game { get; set; }
}