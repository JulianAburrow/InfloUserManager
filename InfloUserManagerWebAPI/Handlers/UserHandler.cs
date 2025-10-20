namespace InfloUserManagerWebAPI.Handlers;

public class UserHandler(InfloUserManagerDbContext context) : IUserHandler
{
    public async Task<List<UserModel>> CheckForExistingUsersAsync(string userNumber, int id)
    {
        var users = await context.Users
            .Where(
                u => u.UserNumber == userNumber)
            .ToListAsync();

        if (id > 0)
        {
            users = [.. users.Where(u => u.UserId != id)];
        }

        return users;
    }

    public async Task<ActionResult> CreateUserAsync(UserDTO userDTO)
    {
        var user = new UserModel
        {
            UserNumber = userDTO.UserNumber,
            Forename = userDTO.Forename,
            Surname = userDTO.Surname,
            Email = userDTO.Email,
            DateOfBirth = userDTO.DateOfBirth,
            StatusId = userDTO.StatusId,
        };

        try
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new CreatedResult($"/api/users/{user.UserId}", userDTO);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult($"An error occurred while creating the user: {ex.Message}");
        }
    }

    public async Task<ActionResult> DeleteUserAsync(int userId)
    {
        var userToDelete = context.Users
            .FirstOrDefault(u => u.UserId == userId);

        if (userToDelete is null)
        {
            return new NotFoundResult();
        }

        try
        {
            context.Users.Remove(userToDelete);
            await context.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult($"An error occurred while deleting the user: {ex.Message}");
        }
    }

    public async Task<UserDTO?> GetUserAsync(int id)
    {
        var user = await context.Users
            .Include(u => u.Status)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (user is null)
        {
            return null;
        }

        var userDTO = new UserDTO
        {
            UserId = user.UserId,
            UserNumber = user.UserNumber,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            StatusId = user.StatusId,
            StatusName = user.Status.StatusName,
        };

        return userDTO;
    }

    public async Task<List<UserDTO>> GetUsersAsync()
    {
        var users = await context.Users
            .Include(c => c.Status)
            .OrderBy(c => c.Surname)
            .AsNoTracking()
            .ToListAsync();

        var userDTOs = new List<UserDTO>();

        foreach (var user in users)
        {
            userDTOs.Add(new UserDTO
            {
                UserId = user.UserId,
                UserNumber = user.UserNumber,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                StatusId = user.StatusId,
                StatusName = user.Status.StatusName,
            });
        }

        return userDTOs;
    }

    public async Task<ActionResult> UpdateUserAsync(int id, UserDTO userDTO)
    {
        var userToUpdate = await context.Users
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (userToUpdate is null)
        {
            return new NotFoundObjectResult("No User with this id could be found");
        }

        userToUpdate.UserNumber = userDTO.UserNumber;
        userToUpdate.Forename = userDTO.Forename;
        userToUpdate.Surname = userDTO.Surname;
        userToUpdate.Email = userDTO.Email;
        userToUpdate.DateOfBirth = userDTO.DateOfBirth;
        userToUpdate.StatusId = userDTO.StatusId;

        try
        {
            await context.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult($"An error occurred while updating the User: {ex.Message}");
        }
    }
}
