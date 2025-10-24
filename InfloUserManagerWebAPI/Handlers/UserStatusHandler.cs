namespace InfloUserManagerWebAPI.Handlers;

public class UserStatusHandler(InfloUserManagerDbContext context) : IUserStatusHandler
{
    public async Task<int> CheckForExistingUserStatusesAsync(string statusName, int id)
    {
        var userStatuses = await context.UserStatuses
            .Where(
                u => u.StatusName == statusName)
            .ToListAsync();

        if (id > 0)
        {
            userStatuses = [.. userStatuses.Where(u => u.StatusId != id)];
        }

        return userStatuses.Count;
    }

    public async Task<ActionResult> CreateUserStatusAsync(UserStatusDTO userStatusDTO)
    {
        var userStatus = new UserStatusModel
        {
            StatusName = userStatusDTO.StatusName,
        };

        try
        {
            context.UserStatuses.Add(userStatus);
            await context.SaveChangesAsync();
            return new CreatedResult($"/api/userstatuses/{userStatus.StatusId}", userStatusDTO);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult($"An error occurred while creating the userstatus: {ex.Message}");
        }
    }

    public async Task<ActionResult> DeleteUserStatusAsync(int id)
    {
        var userStatusToDelete = context.UserStatuses
            .FirstOrDefault(u => u.StatusId == id);

        if (userStatusToDelete is null)
        {
            return new NotFoundResult();
        }

        try
        {
            context.UserStatuses.Remove(userStatusToDelete);
            await context.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult($"An error occurred while deleting the user status: {ex.Message}");
        }
    }

    public async Task<UserStatusDTO?> GetUserStatusAsync(int id)
    {
        var userStatus = await context.UserStatuses
            .Include(u => u.Users)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.StatusId == id);

        if (userStatus is null)
        {
            return null;
        }

        var userStatusDTO = new UserStatusDTO
        {
            StatusId = userStatus.StatusId,
            StatusName = userStatus.StatusName,
            UserCount = userStatus.Users?.Count ?? 0,
        };

        return userStatusDTO;
    }

    public async Task<List<UserStatusDTO>> GetUserStatusesAsync()
    {
        var userStatuses = await context.UserStatuses
            .Include(u => u.Users)
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
                UserCount = userStatus.Users?.Count ?? 0,
            });
        }

        return userStatusDTOs;
    }

    public async Task<ActionResult> UpdateUserStatusAsync(int id, UserStatusDTO userStatusDTO)
    {
        var userStatusToUpdate = await context.UserStatuses
            .FirstOrDefaultAsync(u => u.StatusId == id);

        if (userStatusToUpdate is null)
        {
            return new NotFoundObjectResult("No UserStatus with this id could be found.");
        }

        userStatusToUpdate.StatusName = userStatusDTO.StatusName;

        try
        {
            await context.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult($"An error occurred while updating the UserStatus: {ex.Message}");
        }
    }
}
