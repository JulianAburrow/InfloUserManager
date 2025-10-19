namespace InfloUserManagerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserHandler userHandler) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetUsers()
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
