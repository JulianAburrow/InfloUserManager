namespace InfloUserManagerWebAPI.Handlers;

public class UserStatusHandler(InfloUserManagerDbContext context) : IUserStatusHandler
{

    public async Task<List<UserStatusDTO>> GetUserStatusesAsync()
    {
        var userStatuses = await context.UserStatuses
            .OrderBy(s => s.StatusName)
            .AsNoTracking()
            .ToListAsync();

        var userStatusDTOs = new List<UserStatusDTO>();

        foreach (var userStatus in userStatuses)
        {
            userStatusDTOs.Add(new UserStatusDTO
            {
                StatusId = userStatus.StatusId,
                StatusName = userStatus.StatusName,
            });
        }

        return userStatusDTOs;
    }
}
