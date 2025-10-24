namespace Tests;

public class UserStatusControllerTests
{
    private readonly Mock<IUserStatusHandler> _mockUserStatusHandler;
    private readonly UserStatusController _userStatusController;

    public UserStatusControllerTests()
    {
        _mockUserStatusHandler = new Mock<IUserStatusHandler>();
        _userStatusController = new UserStatusController(_mockUserStatusHandler.Object);
    }

    [Fact]
    public async Task GetUserStatusesAsync_ReturnsListOfUserStatuses()
    {
        var status1 = "Status1";
        var status2 = "Status2";

        var mockUserStatuses = new List<UserStatusDTO>
        {
            new() { StatusName = status1 },
            new() { StatusName = status2 },
        };
        _mockUserStatusHandler.Setup(handler => handler.GetUserStatusesAsync())
            .ReturnsAsync(mockUserStatuses);

        var result = await _userStatusController.GetUserStatusesAsync();
        var returnValue = Assert.IsType<List<UserStatusDTO>>(result);

        Assert.Equal(2, returnValue.Count);
        Assert.Equal(status1, returnValue[0].StatusName);
        Assert.Equal(status2, returnValue[1].StatusName);
    }

    [Fact]
    public async Task GetUserStatusAsync_ReturnsUserStatus()
    {
        var status1 = "Status1";

        var mockUserStatus = new UserStatusDTO
        {
            StatusName = status1,
        };

        _mockUserStatusHandler.Setup(handler => handler.GetUserStatusAsync(1))
            .ReturnsAsync(mockUserStatus);

        var actionResult = await _userStatusController.GetUserStatusAsync(1);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedUserStatus = Assert.IsType<UserStatusDTO>(okResult.Value);

        Assert.Equal(status1, returnedUserStatus.StatusName);
    }

    [Fact]
    public async Task GetUserStatusAsync_ReturnsNotFound_WhenUserStatusNotFound()
    {
        _mockUserStatusHandler.Setup(handler => handler.GetUserStatusAsync(1))
            .ReturnsAsync((UserStatusDTO)null);

        var result = await _userStatusController.GetUserStatusAsync(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateUserStatusAsync_ReturnsOkResult_WhenCreateSuccessful()
    {
        var newUserStatus = new UserStatusDTO { StatusName = "NewStatus" };
        _mockUserStatusHandler.Setup(handler => handler.CreateUserStatusAsync(newUserStatus))
            .ReturnsAsync(new OkResult());

        var result = await _userStatusController.CreateUserStatusAsync(newUserStatus);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task CheckForExistinguserStatusesAsync_ReturnsOk_WhenNoExistingUserStatuses()
    {
        _mockUserStatusHandler.Setup(handler => handler.CheckForExistingUserStatusesAsync("RandomStatusName", 100))
            .ReturnsAsync(0);

        var controller = new UserStatusController(_mockUserStatusHandler.Object);

        var result = await controller.CheckForExistingUserStatusesAsync("RandomStatusName", 100);

        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CheckForExistingUserStatusAsync_ReturnsConflict_WhenUserStatusExists()
    {
        _mockUserStatusHandler
            .Setup(handler => handler.CheckForExistingUserStatusesAsync("ExistingStatusName", 101))
            .ReturnsAsync(2);

        var controller = new UserStatusController(_mockUserStatusHandler.Object);

        var result = await controller.CheckForExistingUserStatusesAsync("ExistingStatusName", 101);

        var conflictResult = Assert.IsType<ConflictResult>(result);
        Assert.Equal((int)HttpStatusCode.Conflict, conflictResult.StatusCode);
    }

    [Fact]
    public async Task UpdateUserStatusAsync_ReturnsActionResult_WhenUpdateSuccessful()
    {
        var updatedUserStatus = new UserStatusDTO { StatusName = "UpdatedStatus" };
        _mockUserStatusHandler.Setup(handler => handler.UpdateUserStatusAsync(1, updatedUserStatus))
            .ReturnsAsync(new OkResult());

        var result = await _userStatusController.UpdateUserStatusAsync(1, updatedUserStatus);
        
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteUserStatusAsync_ReturnsOk_WhenUserStatusExists()
    {
        _mockUserStatusHandler.Setup(handler => handler.DeleteUserStatusAsync(1))
            .ReturnsAsync(new OkResult());

        var controller = new UserStatusController(_mockUserStatusHandler.Object);

        var result = await controller.DeleteUserStatusAsync(1);

        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
    }

    [Fact]
    public async Task DeleteUserStatusAsync_ReturnsNotFound_WhenUserStatusDoesNotExist()
    {
        _mockUserStatusHandler.Setup(handler => handler.DeleteUserStatusAsync(999))
            .ReturnsAsync(new NotFoundObjectResult("UserStatus not found"));

        var controller = new UserStatusController(_mockUserStatusHandler.Object);

        var result = await controller.DeleteUserStatusAsync(999);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        Assert.Equal("UserStatus not found", notFoundResult.Value);
    }
}
