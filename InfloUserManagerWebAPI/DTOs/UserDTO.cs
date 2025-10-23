namespace InfloUserManagerWebAPI.DTOs;

public class UserDTO
{
    public int UserId { get; set; }

    public string UserNumber { get; set; } = string.Empty;

    public string Forename { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string? Email { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int StatusId { get; set; }

    public string StatusName { get; set; } = string.Empty;
}
