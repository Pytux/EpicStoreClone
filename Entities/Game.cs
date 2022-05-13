using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class Game
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }

    [Precision(10, 2)] public decimal Price { get; set; }

    [Precision(3, 2)] public decimal Discount { get; set; }
}