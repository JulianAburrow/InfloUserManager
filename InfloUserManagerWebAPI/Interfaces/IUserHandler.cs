namespace InfloUserManagerWebAPI.Interfaces;

public interface IUserHandler
{
    Task<List<UserModel>> CheckForExistingUsersAsync(string userNumber, int id);

    Task<List<UserDTO>> GetUsersAsync();

    Task<UserDTO?> GetUserAsync(int id);

    Task<ActionResult> CreateUserAsync(UserDTO UserDTO);

    Task<ActionResult> UpdateUserAsync(int id, UserDTO UserDTO);

    Task<ActionResult> DeleteUserAsync(int userId);
}
