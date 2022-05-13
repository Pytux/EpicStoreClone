using System.ComponentModel.DataAnnotations;

namespace Entities;

public class News
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public string Description { get; set; }
    public string Image { get; set; }
    public string Url { get; set; }
}