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
    public async Task GetUsers_ReturnsListOfUsers()
    {
        var mockUsers = new List<UserDTO> { testUsers[0], testUsers[1], };
        _mockUserHandler.Setup(handler => handler.GetUsersAsync())
            .ReturnsAsync(mockUsers);

        var result = await _userController.GetUsers();

        var returnValue = Assert.IsType<List<UserDTO>>(result.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetUser_ReturnsUser()
    {
        // Arrange
        var mockUser = testUsers[2];
        _mockUserHandler.Setup(handler => handler.GetUserAsync(1))
            .ReturnsAsync(mockUser);

        // Act
        var actionResult = await _userController.GetUserAsync(1);

        // Assert
        // Check if the result is OkObjectResult and extract the value
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
        Assert.Equal(3, returnedUser.UserId);
        Assert.Equal("Test3", returnedUser.Forename);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserNotFound()
    {
        _mockUserHandler.Setup(handler => handler.GetUserAsync(1))
            .ReturnsAsync((UserDTO)null);

        var result = await _userController.GetUserAsync(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateUser_ReturnsOkResult_WhenCreateSuccessful()
    {
        var newUser = testUsers[3];
        _mockUserHandler.Setup(handler => handler.CreateUserAsync(newUser))
            .ReturnsAsync(new OkResult());

        var result = await _userController.CreateUserAsync(newUser);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsActionResult_WhenUpdateSuccessful()
    {
        var updatedUser = testUsers[0];
        _mockUserHandler.Setup(handler => handler.UpdateUserAsync(1, updatedUser))
            .ReturnsAsync(new OkResult());

        var result = await _userController.UpdateUserAsync(1, updatedUser);

        Assert.IsType<OkResult>(result);
    }
}
