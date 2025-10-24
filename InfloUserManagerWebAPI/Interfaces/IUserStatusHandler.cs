namespace InfloUserManagerWebAPI.Interfaces;

public interface IUserStatusHandler
{
    Task<int> CheckForExistingUserStatusesAsync(string statusName, int id);

    Task<List<UserStatusDTO>> GetUserStatusesAsync();

    Task<UserStatusDTO?> GetUserStatusAsync(int id);

    Task<ActionResult> CreateUserStatusAsync(UserStatusDTO userStatusDTO);

    Task<ActionResult> UpdateUserStatusAsync(int id, UserStatusDTO userStatusDTO);

    Task<ActionResult> DeleteUserStatusAsync(int id);
}
