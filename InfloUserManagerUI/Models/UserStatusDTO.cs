namespace InfloUserManagerUI.Models;

public class UserStatusDTO
{
    public int StatusId { get; set; }
    
    public string StatusName { get; set; } = null!;

    public int UserCount { get; set; }
}
