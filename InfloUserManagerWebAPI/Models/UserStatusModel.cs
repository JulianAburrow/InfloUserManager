namespace InfloUserManagerWebAPI.Models;

public class UserStatusModel
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = default!;

    public ICollection<UserModel>? Users { get; set; } = null;
}
