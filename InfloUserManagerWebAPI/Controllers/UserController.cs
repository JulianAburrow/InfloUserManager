

namespace InfloUserManagerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserHandler userHandler) : ControllerBase
{
    [HttpGet("checkusers/{userNumber}/{id}")]
    public async Task<IActionResult> CheckForExistingUsersAsync(string userNumber, int id)
    {
        var userCount = await userHandler.CheckForExistingUsersAsync(userNumber, id);

        return userCount == 0 ? Ok() : Conflict();
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetUsersAsync()
    {
        return await userHandler.GetUsersAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserAsync(int id)
    {
        var user = await userHandler.GetUserAsync(id)!;
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUserAsync(UserDTO UserDTO)
    {
        return await userHandler.CreateUserAsync(UserDTO);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserAsync(int id, UserDTO UserDTO)
    {
        return await userHandler.UpdateUserAsync(id, UserDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        return await userHandler.DeleteUserAsync(id);
    }
}
