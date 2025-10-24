using Microsoft.AspNetCore.Mvc;

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

    private readonly List<UserStatusModel> testUserStatusModels = Shared.GetTestUserStatusModels();

    private readonly List<UserStatusDTO> testUserStatusDTOs = Shared.GetTestUserStatusDTOs();

    [Fact]
    public async Task CheckForExistingUserStatuses_ReturnsZero_WhenUserStatusNotPresent()
    {
        await RemoveAllUserStatusesFromContext();

        _context.UserStatuses.AddRange(testUserStatusModels);
        await _context.SaveChangesAsync();

        var result = await _handler.CheckForExistingUserStatusesAsync("Pending", 2);

        Assert.Equal(0, result);

    }

    [Fact]
    public async Task CheckForExistingUserStatuses_ReturnsNonZeroInteger_WhenUserStatusPresent()
    {
        await RemoveAllUserStatusesFromContext();

        _context.UserStatuses.AddRange(testUserStatusModels);
        await _context.SaveChangesAsync();

        var result = await _handler.CheckForExistingUserStatusesAsync("Active", 0);

        Assert.True(result > 0);
    }

    [Fact]
    public async Task GetUserStatusesAsync_GetsAllUserStatuses()
    {
        await RemoveAllUserStatusesFromContext();

        _context.UserStatuses.AddRange(testUserStatusModels);
        await _context.SaveChangesAsync();
        var result = await _handler.GetUserStatusesAsync();

        Assert.Equal(_context.UserStatuses.Count(), result.Count);
    }

    [Fact]
    public async Task GetUserStatusAsync_Returns_CorrectUserStatus()
    {
        await RemoveAllUserStatusesFromContext();

        _context.UserStatuses.Add(testUserStatusModels[0]);
        await _context.SaveChangesAsync();

        var result = await _handler.GetUserStatusAsync(testUserStatusModels[0].StatusId);

        Assert.NotNull(result);
        Assert.Equal(testUserStatusModels[0].StatusName, result.StatusName);
        Assert.Equal(1, result.StatusId);
    }

    [Fact]
    public async Task GetUserStatusAsync_ReturnsNull_WhenUserStatusDoesNotExist()
    {
        await RemoveAllUserStatusesFromContext();

        var result = await _handler.GetUserStatusAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateUserStatusAsync_ReturnsCreatedResult()
    {
        await RemoveAllUserStatusesFromContext();

        var result = await _handler.CreateUserStatusAsync(testUserStatusDTOs[0]);

        var createdResult = Assert.IsType<CreatedResult>(result);
        var createdUserStatus = await _context.UserStatuses
            .FirstOrDefaultAsync(c => c.StatusName == testUserStatusDTOs[0].StatusName);

        Assert.NotNull(createdUserStatus);
        Assert.Equal(testUserStatusDTOs[0].StatusName, createdUserStatus.StatusName);
    }

    [Fact]
    public async Task UpdateUserStatusAsync_ReturnsOk()
    {
        await RemoveAllUserStatusesFromContext();

        _context.UserStatuses.Add(testUserStatusModels[0]);
        await _context.SaveChangesAsync();

        var createdUserStatus = await _context.UserStatuses
            .FirstOrDefaultAsync(c => c.StatusId == testUserStatusModels[0].StatusId);
        Assert.NotNull(createdUserStatus);

        var result = await _handler.UpdateUserStatusAsync(createdUserStatus.StatusId, testUserStatusDTOs[1]);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateUserStatusAsync_ReturnsNotFound_WhenUserStatusDoesNotExist()
    {
        var result = await _handler.UpdateUserStatusAsync(999, testUserStatusDTOs[1]);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUserStatusAsync_DeletesUserStatus()
    {
        await RemoveAllUserStatusesFromContext();

        _context.UserStatuses.Add(testUserStatusModels[0]);
        await _context.SaveChangesAsync();

        var createdUserStatus = await _context.UserStatuses.FindAsync(testUserStatusModels[0].StatusId);
        Assert.NotNull(createdUserStatus);
        
        var result = await _handler.DeleteUserStatusAsync(createdUserStatus.StatusId);
        
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        var deletedUserStatus = await _context.UserStatuses
            .FirstOrDefaultAsync(c => c.StatusId == createdUserStatus.StatusId);
        Assert.Null(deletedUserStatus);
    }

    [Fact]
    public async Task DeleteUserStatusAsync_ReturnsNotFound_WhenUserStatusDoesNotExist()
    {
        var result = await _handler.DeleteUserStatusAsync(999);
        Assert.IsType<NotFoundResult>(result);
    }

    private async Task RemoveAllUserStatusesFromContext()
    {
        _context.UserStatuses.RemoveRange(_context.UserStatuses);
        await _context.SaveChangesAsync();
    }
}