namespace InfloUserManagerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserStatusController(IUserStatusHandler userStatusHandler) : ControllerBase
{
    [HttpGet("checkuserstatuses/{userstatus}/{id}")]
    public async Task<IActionResult> CheckForExistingUserStatusesAsync(string userStatus, int id)
    {
        var userStatusesCount = await userStatusHandler.CheckForExistingUserStatusesAsync(userStatus, id);

        return userStatusesCount == 0 ? Ok() : Conflict();
    }

    [HttpGet]
    public async Task<List<UserStatusDTO>> GetUserStatusesAsync()
    {
        return await userStatusHandler.GetUserStatusesAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserStatusDTO>> GetUserStatusAsync(int id)
    {
        var userstatus = await userStatusHandler.GetUserStatusAsync(id)!;
        if (userstatus == null)
        {
            return NotFound();
        }
        return Ok(userstatus);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUserStatusAsync(UserStatusDTO UserStatusDTO)
    {
        return await userStatusHandler.CreateUserStatusAsync(UserStatusDTO);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserStatusAsync(int id, UserStatusDTO UserStatusDTO)
    {
        return await userStatusHandler.UpdateUserStatusAsync(id, UserStatusDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserStatusAsync(int id)
    {
        return await userStatusHandler.DeleteUserStatusAsync(id);
    }
}
