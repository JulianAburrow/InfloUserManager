namespace Tests;

public static class Shared
{
    public static InfloUserManagerDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<InfloUserManagerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new InfloUserManagerDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }

    public static List<UserDTO> GetTestUserDTOs()
    {
        return
        [
            CreateUserDTO1(),
            CreateUserDTO2(),
            CreateUserDTO3(),
            CreateUserDTO4(),
        ];
    }

    private static UserDTO CreateUserDTO1() => new()
    {
        UserNumber = "U001",
        Forename = "Test1",
        Surname = "User1",
        Email = "testuser1@test.com",
        DateOfBirth = new DateTime(1990, 1, 1),
        StatusId = 1,
    };

    private static UserDTO CreateUserDTO2() => new()
    {
        UserNumber = "U002",
        Forename = "Test2",
        Surname = "User2",
        Email = "testuser2@test.com",
        DateOfBirth = new DateTime(1991, 1, 1),
        StatusId = 2,
    };

    private static UserDTO CreateUserDTO3() => new()
    {
        UserNumber = "U003",
        Forename = "Test3",
        Surname = "User3",
        Email = "testuser3@test.com",
        DateOfBirth = new DateTime(1992, 1, 1),
        StatusId = 1,
    };

    private static UserDTO CreateUserDTO4() => new()
    {
        UserNumber = "U004",
        Forename = "Test4",
        Surname = "User4",
        Email = "testuser4@test.com",
        DateOfBirth = new DateTime(1993, 1, 1),
        StatusId = 2,
    };

    public static List<UserModel> GetTestUserModels()
    {
        return
        [
            CreateUserModel1(),
            CreateUserModel2(),
            CreateUserModel3(),
            CreateUserModel4(),
        ];
    }

    private static UserModel CreateUserModel1() => new()
    {
        UserNumber = "U001",
        Forename = "Test1",
        Surname = "User1",
        Email = "testuser1@test.com",
        DateOfBirth = new DateTime(1990, 1, 1),
        StatusId = 1,
        Status = new UserStatusModel
        {
            StatusName = "Active",
        },
    };

    private static UserModel CreateUserModel2() => new()
    {
        UserNumber = "U002",
        Forename = "Test2",
        Surname = "User2",
        Email = "testuser2@test.com",
        DateOfBirth = new DateTime(1991, 1, 1),
        StatusId = 2,
        Status = new UserStatusModel
        {
            StatusName = "Inactive",
        },
    };

    private static UserModel CreateUserModel3() => new()
    {
        UserNumber = "U003",
        Forename = "Test3",
        Surname = "User3",
        Email = "testuser3@test.com",
        DateOfBirth = new DateTime(1992, 1, 1),
        StatusId = 1,
        Status = new UserStatusModel
        {
            StatusName = "Active",
        },
    };

    private static UserModel CreateUserModel4() => new()
    {
        UserNumber = "U004",
        Forename = "Test4",
        Surname = "User4",
        Email = "testuser4@test.com",
        DateOfBirth = new DateTime(1993, 1, 1),
        StatusId = 2,
        Status = new UserStatusModel
        {
            StatusName = "Inactive",
        },
    };
}
