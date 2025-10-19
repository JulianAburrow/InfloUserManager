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
    public async Task GetUserStatuses_ReturnsListOfUserStatuses()
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

        var result = await _userStatusController.GetUserStatuses();
        var returnValue = Assert.IsType<List<UserStatusDTO>>(result);

        Assert.Equal(2, returnValue.Count);
        Assert.Equal(status1, returnValue[0].StatusName);
        Assert.Equal(status2, returnValue[1].StatusName);
    }
}
