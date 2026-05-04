using System.ComponentModel.DataAnnotations;

namespace EventEase.Server.Models;

public class Registration
{
    public int Id { get; set; }
    public int EventId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Range(1, 10)]
    public int Tickets { get; set; } = 1;
}
