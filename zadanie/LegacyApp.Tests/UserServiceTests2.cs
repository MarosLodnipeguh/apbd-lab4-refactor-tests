namespace LegacyApp.Tests;

public class UserServiceTests2
{
    [Fact]
    public void AddUser_ThrowsExceptionWhenClientDoesNotExist()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        Action action = () => userService.AddUser(
            null,
            "Kowalski",
            "kowalski@g.com",
            DateTime.Parse("2000-01-01"),
            100
        );
        
        // Assert
        Assert.Throws<ArgumentException>(action);
    }
}