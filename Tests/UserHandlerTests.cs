namespace Tests;

public class UserHandlerTests
{
    private readonly UserHandler _handler;
    private readonly InfloUserManagerDbContext _context;

    public UserHandlerTests()
    {
        _context = Shared.GetInMemoryDbContext();
        _handler = new UserHandler(_context);
    }

    private readonly List<UserModel> testUserModels = Shared.GetTestUserModels();

    private readonly List<UserDTO> testUserDTOs = Shared.GetTestUserDTOs();

    [Fact]
    public async Task CheckForExistingUsers_ReturnsZero_WhenUserNotPresent()
    {
        await RemoveAllUsersAndUserStatusesFromContext();

        _context.Users.AddRange(testUserModels);
        await _context.SaveChangesAsync();

        var result = await _handler.CheckForExistingUsersAsync("U042", 2);

        Assert.Equal(0, result);
    }

    [Fact]
    public async Task CheckForExistingUsers_ReturnsNonZeroInteger_WhenUserPresent()
    {
        await RemoveAllUsersAndUserStatusesFromContext();

        _context.Users.AddRange(testUserModels);
        await _context.SaveChangesAsync();

        var result = await _handler.CheckForExistingUsersAsync("U002", 0);

        Assert.True(result > 0);
    }

    [Fact]
    public async Task GetUsersAsync_GetsAllUsers()
    {
        await RemoveAllUsersAndUserStatusesFromContext();

        _context.Users.AddRange(testUserModels);
        await _context.SaveChangesAsync();

        var result = await _handler.GetUsersAsync();

        Assert.Equal(4, result.Count);
        Assert.Equal(testUserModels[0].Forename, result[0].Forename);
        Assert.Equal(testUserModels[1].Forename, result[1].Forename);
        Assert.Equal(testUserModels[2].Forename, result[2].Forename);
        Assert.Equal(testUserModels[3].Forename, result[3].Forename);
    }

    [Fact]
    public async Task GetUserAsync_GetsCorrectUser()
    {
        await RemoveAllUsersAndUserStatusesFromContext();

        _context.Users.Add(testUserModels[0]);
        await _context.SaveChangesAsync();

        var result = await _handler.GetUserAsync(1);

        Assert.NotNull(result);
        Assert.Equal(testUserModels[0].Forename, result.Forename);
        Assert.Equal(1, result.UserId);
    }

    [Fact]
    public async Task GetUserAsync_ReturnsNull_WhenUserDoesNotExist()
    {
        await RemoveAllUsersAndUserStatusesFromContext();

        var result = await _handler.GetUserAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateUserAsync_ReturnsCreatedResult()
    {
        await RemoveAllUsersAndUserStatusesFromContext();

        var result = await _handler.CreateUserAsync(testUserDTOs[0]);

        var createdResult = Assert.IsType<CreatedResult>(result);
        var createdUser = await _context.Users
            .FirstOrDefaultAsync(c => c.UserId == 1);
        Assert.NotNull(createdUser);
        Assert.Equal(testUserDTOs[0].Forename, createdUser.Forename);
    }

    [Fact]
    public async Task UpdateUserAsync_ReturnsOk()
    {
       await RemoveAllUsersAndUserStatusesFromContext();

        _context.Users.Add(testUserModels[3]);            
        await _context.SaveChangesAsync();

        var createdUser = await _context.Users
            .FirstOrDefaultAsync(c => c.UserId == testUserModels[3].UserId);
        Assert.NotNull(createdUser);

        var result = await _handler.UpdateUserAsync(createdUser.UserId, testUserDTOs[2]);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateUserAsync_ReturnsNotFound_WhenUserDoesNotExist()
    {
        var result = await _handler.UpdateUserAsync(999, testUserDTOs[3]);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    private async Task RemoveAllUsersAndUserStatusesFromContext()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.UserStatuses.RemoveRange(_context.UserStatuses);
        await _context.SaveChangesAsync();
    }
}
