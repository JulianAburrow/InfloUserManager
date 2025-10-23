namespace InfloUserManagerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserStatusController(IUserStatusHandler userStatusHandler) : ControllerBase
{
    [HttpGet]
    public async Task<List<UserStatusDTO>> GetUserStatusesAsync()
    {
        return await userStatusHandler.GetUserStatusesAsync();
    }
}
