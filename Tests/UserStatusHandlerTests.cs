
namespace Tests;

public class UserStatusHandlerTests
{
    private readonly UserStatusHandler _handler;
    private readonly InfloUserManagerDbContext _context;

    public UserStatusHandlerTests()
    {
        _context = Shared.GetInMemoryDbContext();
        _handler = new UserStatusHandler(_context);
    }

    [Fact]
    public async Task GetUserStatusesAsync_GetsAllUserStatuses()
    {
        await RemoveAllUserStatusesFromContext();

        var testUserStatus1 = "TestUserStatus1";
        var testUserStatus2 = "TestUserStatus2";

        _context.UserStatuses.AddRange(new List<UserStatusModel>
        {
            new() { StatusName = testUserStatus1 },
            new() { StatusName = testUserStatus2 },
        });
        await _context.SaveChangesAsync();
        var result = await _handler.GetUserStatusesAsync();

        Assert.Equal(2, result.Count);
        Assert.Equal(testUserStatus1, result[0].StatusName);
        Assert.Equal(testUserStatus2, result[1].StatusName);
    }

    private async Task RemoveAllUserStatusesFromContext()
    {
        _context.UserStatuses.RemoveRange(_context.UserStatuses);
        await _context.SaveChangesAsync();
    }
}
