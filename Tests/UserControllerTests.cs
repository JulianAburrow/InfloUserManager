namespace Tests;

public class UserControllerTests
{
    private readonly Mock<IUserHandler> _mockUserHandler;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _mockUserHandler = new Mock<IUserHandler>();
        _userController = new UserController(_mockUserHandler.Object);
    }

    private readonly List<UserDTO> testUsers = Shared.GetTestUserDTOs();

    [Fact]
    public async Task GetUsersAsync_ReturnsListOfUsers()
    {
        var mockUsers = new List<UserDTO> { testUsers[0], testUsers[1], };
        _mockUserHandler.Setup(handler => handler.GetUsersAsync())
            .ReturnsAsync(mockUsers);

        var result = await _userController.GetUsersAsync();

        var returnValue = Assert.IsType<List<UserDTO>>(result.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetUserAsync_ReturnsUser()
    {
        var mockUser = testUsers[2];
        _mockUserHandler.Setup(handler => handler.GetUserAsync(1))
            .ReturnsAsync(mockUser);

        var actionResult = await _userController.GetUserAsync(1);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
        Assert.Equal("Test3", returnedUser.Forename);
    }

    [Fact]
    public async Task GetUserAsync_ReturnsNotFound_WhenUserNotFound()
    {
        _mockUserHandler.Setup(handler => handler.GetUserAsync(1))
            .ReturnsAsync((UserDTO)null);

        var result = await _userController.GetUserAsync(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateUserAsync_ReturnsOkResult_WhenCreateSuccessful()
    {
        var newUser = testUsers[3];
        _mockUserHandler.Setup(handler => handler.CreateUserAsync(newUser))
            .ReturnsAsync(new OkResult());

        var result = await _userController.CreateUserAsync(newUser);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task CheckForExistingUsersAsync_ReturnsOk_WhenNoExistingUsers()
    {
        _mockUserHandler.Setup(handler => handler.CheckForExistingUsersAsync(testUsers[0].UserNumber, testUsers[0].UserId))
            .ReturnsAsync(new List<UserModel>()); // Fix: Return List<UserModel> as per interface

        var controller = new UserController(_mockUserHandler.Object);

        var result = await controller.CheckForExistingUsersAsync(testUsers[0].UserNumber, testUsers[0].UserId);

        var okResult = Assert.IsType<OkResult>(result.Result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CheckForExistingUsersAsync_ReturnsConflict_WhenUsersExist()
    {
        _mockUserHandler.Setup(handler => handler.CheckForExistingUsersAsync(testUsers[1].UserNumber, testUsers[1].UserId))
            .ReturnsAsync([new() { }]);

        var controller = new UserController(_mockUserHandler.Object);

        var result = await controller.CheckForExistingUsersAsync(testUsers[1].UserNumber, testUsers[1].UserId);

        var conflictResult = Assert.IsType<ConflictResult>(result.Result);
        Assert.Equal((int)HttpStatusCode.Conflict, conflictResult.StatusCode);
    }

    [Fact]
    public async Task UpdateUserAsync_ReturnsActionResult_WhenUpdateSuccessful()
    {
        var updatedUser = testUsers[0];
        _mockUserHandler.Setup(handler => handler.UpdateUserAsync(1, updatedUser))
            .ReturnsAsync(new OkResult());

        var result = await _userController.UpdateUserAsync(1, updatedUser);

        Assert.IsType<OkResult>(result);
    }
}
