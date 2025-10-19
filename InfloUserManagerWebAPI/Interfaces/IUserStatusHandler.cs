namespace InfloUserManagerWebAPI.Interfaces;

public interface IUserStatusHandler
{
    Task<List<UserStatusDTO>> GetUserStatusesAsync();
}
