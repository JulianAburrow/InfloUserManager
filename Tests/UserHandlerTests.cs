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
    public async Task GetUsers_GetsAllUsers()
    {
        await RemoveAllUsersFromContext();

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
    public async Task GetUser_GetsCorrectUser()
    {
        await RemoveAllUsersFromContext();

        _context.Users.Add(testUserModels[1]);
        await _context.SaveChangesAsync();

        var result = await _handler.GetUserAsync(2);

        Assert.NotNull(result);
        Assert.Equal(testUserModels[1].Forename, result.Forename);
        Assert.Equal(2, result.UserId);
    }

    [Fact]
    public async Task GetUser_ReturnsNull_WhenUserDoesNotExist()
    {
        await RemoveAllUsersFromContext();

        var result = await _handler.GetUserAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedResult_()
    {
        await RemoveAllUsersFromContext();

        var result = await _handler.CreateUserAsync(testUserDTOs[0]);

        var createdResult = Assert.IsType<CreatedResult>(result);
        var createdUser = await _context.Users
            .FirstOrDefaultAsync(c => c.Forename == testUserDTOs[0].Forename);
        Assert.NotNull(createdUser);
        Assert.Equal(testUserDTOs[0].Forename, createdUser.Forename);
    }

    [Fact]
    public async Task UpdateUser_ReturnsOk()
    {
       await RemoveAllUsersFromContext();

        _context.Users.Add(testUserModels[1]);            
        await _context.SaveChangesAsync();

        var createdUser = await _context.Users
            .FirstOrDefaultAsync(c => c.Forename == testUserDTOs[1].Forename);
        Assert.NotNull(createdUser);

        var result = await _handler.UpdateUserAsync(createdUser.UserId, testUserDTOs[2]);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        var result = await _handler.UpdateUserAsync(999, testUserDTOs[3]);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    private async Task RemoveAllUsersFromContext()
    {
        _context.Users.RemoveRange(_context.Users);
        await _context.SaveChangesAsync();
    }
}
