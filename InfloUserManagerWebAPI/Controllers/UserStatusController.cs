namespace InfloUserManagerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserStatusController(IUserStatusHandler userStatusHandler) : ControllerBase
{
    [HttpGet]
    public async Task<List<UserStatusDTO>> GetUserStatuses()
    {
        return await userStatusHandler.GetUserStatusesAsync();
    }
}
