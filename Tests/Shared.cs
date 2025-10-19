using InfloUserManagerWebAPI.Models;

namespace Tests;

public static class Shared
{
    public static InfloUserManagerDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<InfloUserManagerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new InfloUserManagerDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }

    public static List<UserDTO> GetTestUserDTOs()
    {
        return
        [
            UserDTO1,
            UserDTO2,
            UserDTO3,
            UserDTO4
        ];
    }

    private static readonly UserDTO UserDTO1 = new()
    {
        UserId = 1,
        Forename = "Test1",
        Surname = "User1",
        Email = "testuser1@test.com",
        DateOfBirth = new DateTime(1990, 1, 1),
        StatusId = 1,
    };

    private static readonly UserDTO UserDTO2 = new()
    {
        UserId = 2,
        Forename = "Test2",
        Surname = "User2",
        Email = "testuser2@test.com",
        DateOfBirth = new DateTime(1991, 1, 1),
        StatusId = 2,
    };

    private static readonly UserDTO UserDTO3 = new()
    {
        UserId = 3,
        Forename = "Test3",
        Surname = "User3",
        Email = "testuser3@test.com",
        DateOfBirth = new DateTime(1992, 1, 1),
        StatusId = 1,
    };

    private static readonly UserDTO UserDTO4 = new()
    {
        UserId = 4,
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
            UserModel1,
            UserModel2,
            UserModel3,
            UserModel4
        ];
    }

    private static readonly UserModel UserModel1 = new()
    {
        UserId = 1,
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

    private static readonly UserModel UserModel2 = new()
    {
        UserId = 2,
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

    private static readonly UserModel UserModel3 = new()
    {
        UserId = 3,
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

    private static readonly UserModel UserModel4 = new()
    {
        UserId = 4,
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
