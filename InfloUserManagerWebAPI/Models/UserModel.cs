namespace InfloUserManagerWebAPI.Models;

public class UserModel
{
    public int UserId { get; set; }

    public string UserNumber { get; set; } = string.Empty;

    public string Forename { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string? Email { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int StatusId { get; set; }

    public UserStatusModel Status { get; set; } = null!;
}
